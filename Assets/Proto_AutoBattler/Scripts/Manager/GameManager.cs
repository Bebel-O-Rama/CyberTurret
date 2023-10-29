using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityHFSM;

public class GameManager : MonoBehaviour
{
    [Header("Editable Game Variables")]
    [SerializeField] public List<RoomData> roomData;
    [SerializeField] [Min(1)] public int podMaxHP = 1;
    
    [Header("READ ONLY")]
    [SerializeField] public string currentRoomName;
    [SerializeField] public List<Enemy> currentRoomEnemies;
    
    // Hidden Variables
    private Player _player;
    private List<Enemy> _enemies;
    private bool _isCurrentRoomLoaded;
    private bool _isCurrentRoomActive;
    private bool _isCurrentRoomCleared;
    private int _podCurrentHP;

    // State Machine & States
    private StateMachine _mainSM;
    private const string LoadRoom = "LoadRoom";
    private const string Planning = "Planning";
    private const string Combat = "Combat";
    private const string Death = "Death";
    private const string Cleared = "Cleared";

    public static GameManager Instance => _instance ??= new GameManager();
    private static GameManager _instance;

    private GameManager()
    {
    }

    private void Awake()
    {
        _instance = this;
        InitializeVariables();
        SetupSM();
    }

    private void Update()
    {
        _mainSM.OnLogic();
        // Shit code to test stuff
        if (Input.GetKey(KeyCode.Z))
            _isCurrentRoomActive = true;
        if (Input.GetKey(KeyCode.X))
            _isCurrentRoomCleared = true;
    }
    
    public void DamagePod(int hitDmg)
    {
        _podCurrentHP -= hitDmg;
        if (_podCurrentHP > 0)
            PodDmgFeedback();
    }

    private void InitializeVariables()
    {
        _player = FindObjectOfType<Player>();
        _isCurrentRoomLoaded = false;
        _isCurrentRoomActive = false;
        _isCurrentRoomCleared = false;
        _podCurrentHP = podMaxHP;
    }

    private void SetupSM()
    {
        _mainSM = new StateMachine();

        _mainSM.AddState(LoadRoom, new State(onEnter: _ => { LoadRoomStateEnter(); }));
        _mainSM.AddState(Planning, new State(onEnter: _ => { PlanningStateEnter(); }));
        _mainSM.AddState(Combat, new State(onEnter: _ => { CombatStateEnter(); }));
        _mainSM.AddState(Death, new State(onEnter: _ => { DeathStateEnter(); }));
        _mainSM.AddState(Cleared, new State(onEnter: _ => { ClearedStateEnter(); }));

        _mainSM.AddTransition(Combat, Death, transition => _podCurrentHP <= 0);
        _mainSM.AddTransition(Combat, Cleared, transition => _isCurrentRoomCleared);
        _mainSM.AddTransition(LoadRoom, Planning, transition => _isCurrentRoomLoaded);
        _mainSM.AddTransition(Planning, Combat, transition => _isCurrentRoomActive);
        
        _mainSM.SetStartState(LoadRoom);
        _mainSM.Init();
    }

    private void PodDmgFeedback()
    {
    }

    #region State Logic (_mainSM)

    private void LoadRoomStateEnter()
    {
        Debug.Log("Enter loading state");
        // Hardcoded the first room to be read, will be updated once we can manage multiple rooms
        roomData[0].SpawnEnemy();
        // Once that's done, update the room's name
        currentRoomName = roomData[0].roomName;
        // And then, fetch all the instance of "Enemy" in the game (I know, it's a lot of "Find", but for now whatever... 
        currentRoomEnemies = new List<Enemy>(FindObjectsByType<Enemy>(FindObjectsSortMode.None));
        
        // Set the currentRoom as loaded
        _isCurrentRoomLoaded = true;
    }

    private void PlanningStateEnter()
    {
        Debug.Log("Enter planning state");
    }

    private void CombatStateEnter()
    {
        Debug.Log("Enter combat state");
        foreach (var enemy in currentRoomEnemies)
        {
            enemy.Activate();
        }
    }

    private void DeathStateEnter()
    {
        Debug.Log("Enter death state");
    }

    private void ClearedStateEnter()
    {
        Debug.Log("Enter clear state");
    }

    #endregion

    // PREVIOUS GAMEMANAGER CODE
    //    
//     // Dependencies
//     [SerializeField] private MainUIBehaviour mainUI;
//
//     // Game info
//     [SerializeField] private int priceNextTurret;
//     [SerializeField] private int currentWaveNumber;
//     [SerializeField] private int currentWaveEnemyLeft;
//     [SerializeField] private int finalWaveNumber;
//     [Min(0f)] [SerializeField] private float delayBetweenWave;
//     [SerializeField] private int currentNumberEnemyAlive;
//     private List<GameObject> currentEnemyAlive = new();
//     [SerializeField] private GameObject turretPF;
//
//     // Player info
//     [SerializeField] private int scrapScore;
//
//     private Objective currentObjective;
//     private Player player;
//     private Transform turretHolder;
//
//     public static GameManager Instance => _instance ??= new GameManager();
//     private static GameManager _instance;
//
//     private GameManager()
//     {
//     }
//
//     private void Awake()
//     {
//         _instance = this;
//
//         InitialSetup();
//     }
//     
//     private void Start()
//     {
//         UpdateCurrentObjective();
//         player = FindObjectOfType<Player>();
//         turretHolder = FindObjectOfType<TurretHolder>().transform;
//
//         finalWaveNumber = EnemySpawnerManager.Instance.GetLastWaveNumber();
//
//         if (currentWaveNumber < 0)
//             currentWaveNumber = 0;
//         PrepareNewWave(0f);
//     }
//
//     private void InitialSetup()
//     {
//         mainUI.SetScrapText(scrapScore);
//         mainUI.SetNextTurretPriceText(priceNextTurret);
//     }
//
//     private void PrepareNewWave(float delay)
//     {
//         if (currentWaveNumber >= finalWaveNumber)
//         {
//             Debug.LogWarning(
//                 "We are trying to access a wave number higher than what we have. Will replay the last wave instead");
//             currentWaveNumber -= 1;
//         }
//
//         currentNumberEnemyAlive = 0;
//         Invoke("StartNewWave", delay);
//     }
//
//     private void StartNewWave()
//     {
//         mainUI.SetCurrentWaveText(EnemySpawnerManager.Instance.GetWaveName(currentWaveNumber));
//         currentWaveEnemyLeft = EnemySpawnerManager.Instance.GetTotalEnemyCountForWave(currentWaveNumber);
//         EnemySpawnerManager.Instance.InitiateNewWave(currentWaveNumber);
//     }
//
//     private void UpdateCurrentObjective()
//     {
//         currentObjective = FindObjectOfType<Objective>();
//     }
//
//     public void WantNewTurret()
//     {
//         if (scrapScore < priceNextTurret)
//             return;
//         SpawnNewTurret();
//         DecrementScraps(priceNextTurret);
//         IncreaseTurretPrice();
//     }
//
//     private void SpawnNewTurret()
//     {
//         GameObject newTurret = Instantiate(turretPF, player.GetPlayerPosition(), Quaternion.identity);
//         newTurret.transform.parent = turretHolder.transform;
//     }
//
//     private void IncreaseTurretPrice()
//     {
//         priceNextTurret += 3;
//         mainUI.SetNextTurretPriceText(priceNextTurret);
//     }
//     
//     public void OnEnemySpawn(GameObject newEnemy)
//     {
//         currentEnemyAlive.Add(newEnemy);
//         currentNumberEnemyAlive += 1;
//     }
//
//     public void OnEnemyKilled(GameObject enemy, int scrapValue)
//     {
//         IncrementScraps(scrapValue);
//         currentEnemyAlive.Remove(enemy);
//         currentWaveEnemyLeft -= 1;
//         currentNumberEnemyAlive -= 1;
//         if (currentWaveEnemyLeft == 0)
//         {
//             currentWaveNumber += 1;
//             PrepareNewWave(delayBetweenWave);
//         }
//     }
//
//     public List<GameObject> GetEnemyList()
//     {
//         return currentEnemyAlive;
//     }
//
//     private void IncrementScraps(int increment)
//     {
//         scrapScore += increment;
//         mainUI.SetScrapText(scrapScore);
//     }
//
//     private void DecrementScraps(int decrement)
//     {
//         scrapScore -= decrement;
//         mainUI.SetScrapText(scrapScore);
//     }
//
//
// public Vector3 GetCurrentObjectivePosition()
//     {
//         return currentObjective.transform.position;
//     }
}