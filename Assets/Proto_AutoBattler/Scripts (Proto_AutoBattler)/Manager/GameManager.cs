using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Dependencies
    [SerializeField] private MainUIBehaviour mainUI;

    // Game info
    [SerializeField] private int priceNextTurret;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private int currentWaveEnemyLeft;
    [SerializeField] private int finalWaveNumber;
    [Min(0f)] [SerializeField] private float delayBetweenWave;
    [SerializeField] private int currentNumberEnemyAlive;
    private List<GameObject> currentEnemyAlive = new();
    [SerializeField] private GameObject turretPF;

    // Player info
    [SerializeField] private int scrapScore;

    private Objective currentObjective;
    private Player player;
    private Transform turretHolder;

    public static GameManager Instance => _instance ??= new GameManager();
    private static GameManager _instance;

    private GameManager()
    {
    }

    private void Awake()
    {
        _instance = this;

        InitialSetup();
    }
    
    private void Start()
    {
        UpdateCurrentObjective();
        player = FindObjectOfType<Player>();
        turretHolder = FindObjectOfType<TurretHolder>().transform;

        finalWaveNumber = EnemySpawnerManager.Instance.GetLastWaveNumber();

        if (currentWaveNumber < 0)
            currentWaveNumber = 0;
        PrepareNewWave(0f);
    }

    private void InitialSetup()
    {
        mainUI.SetScrapText(scrapScore);
        mainUI.SetNextTurretPriceText(priceNextTurret);
    }

    private void PrepareNewWave(float delay)
    {
        if (currentWaveNumber >= finalWaveNumber)
        {
            Debug.LogWarning(
                "We are trying to access a wave number higher than what we have. Will replay the last wave instead");
            currentWaveNumber -= 1;
        }

        currentNumberEnemyAlive = 0;
        Invoke("StartNewWave", delay);
    }

    private void StartNewWave()
    {
        mainUI.SetCurrentWaveText(EnemySpawnerManager.Instance.GetWaveName(currentWaveNumber));
        currentWaveEnemyLeft = EnemySpawnerManager.Instance.GetTotalEnemyCountForWave(currentWaveNumber);
        EnemySpawnerManager.Instance.InitiateNewWave(currentWaveNumber);
    }

    private void UpdateCurrentObjective()
    {
        currentObjective = FindObjectOfType<Objective>();
    }

    public void WantNewTurret()
    {
        if (scrapScore < priceNextTurret)
            return;
        SpawnNewTurret();
        DecrementScraps(priceNextTurret);
        IncreaseTurretPrice();
    }

    private void SpawnNewTurret()
    {
        GameObject newTurret = Instantiate(turretPF, player.GetPlayerPosition(), Quaternion.identity);
        newTurret.transform.parent = turretHolder.transform;
    }

    private void IncreaseTurretPrice()
    {
        priceNextTurret += 3;
        mainUI.SetNextTurretPriceText(priceNextTurret);
    }
    
    public void OnEnemySpawn(GameObject newEnemy)
    {
        currentEnemyAlive.Add(newEnemy);
        currentNumberEnemyAlive += 1;
    }

    public void OnEnemyKilled(GameObject enemy, int scrapValue)
    {
        IncrementScraps(scrapValue);
        currentEnemyAlive.Remove(enemy);
        currentWaveEnemyLeft -= 1;
        currentNumberEnemyAlive -= 1;
        if (currentWaveEnemyLeft == 0)
        {
            currentWaveNumber += 1;
            PrepareNewWave(delayBetweenWave);
        }
    }

    public List<GameObject> GetEnemyList()
    {
        return currentEnemyAlive;
    }

    private void IncrementScraps(int increment)
    {
        scrapScore += increment;
        mainUI.SetScrapText(scrapScore);
    }

    private void DecrementScraps(int decrement)
    {
        scrapScore -= decrement;
        mainUI.SetScrapText(scrapScore);
    }


public Vector3 GetCurrentObjectivePosition()
    {
        return currentObjective.transform.position;
    }
}