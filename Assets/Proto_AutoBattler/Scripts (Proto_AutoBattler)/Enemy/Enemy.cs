using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    // Enemy Data
    [SerializeField] private EnemyData _enemyData; //TODO Find a better way to fill the data

    // State Machines & States
    private StateMachine _mainSM;
    private const string Sleep = "Sleep";
    private const string Activation = "Activation";
    private const string Search = "Search";
    private const string Attack = "Attack";
    private const string Death = "Death";
    
    // Triggers
    private const string RoomActivated = "RoomActivated";
    
    // Current variables
    private Rigidbody2D _rb;
    private Material _mat;
    private bool _isActive;
    private NavMeshAgent _agent;
    
    private int _currentHP;
    private float _currentSpeed;
    private Vector3 _targetPosition;

    private void Update()
    {
        _mainSM.OnLogic();
        
    }
    
    public void OnInstantiation(EnemyData enemyData)
    {
        _enemyData = enemyData;
        InitializeEnemyVariables();
        SetupSM();
    }

    // Called by outside when the room gets activated by the player, starting the fight.
    public void Activate()
    {
        _mainSM.Trigger(RoomActivated);
    }
    
    // We could add stuff like "DmgType" here. It's the method that should be called from outside when the enemy takes damage 
    public void OnHit(int hitDmg)
    {
        var naturalDmg = HitMitigation(hitDmg);
        _currentHP -= naturalDmg;
    }
    
    private void InitializeEnemyVariables()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mat = GetComponent<SpriteRenderer>().material;
        _currentHP = _enemyData.baseHP;
        _currentSpeed = _enemyData.baseSpeed;
        
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        
        _isActive = false;
        _mat.color = _enemyData.sleepingColor;
    }

    private void SetupSM()
    {
        _mainSM = new StateMachine();

        _mainSM.AddState(Sleep, new State(onEnter: _ => { SleepStateEnter(); }));
        _mainSM.AddState(Activation, new State(
            onEnter: _ => { ActivationStateEnter(); },
            onExit: _ => { ActivationStateExit(); }));
        _mainSM.AddState(Search, new State(onEnter: _ => { SearchStateEnter(); }));
        _mainSM.AddState(Attack, new State(onEnter: _ => { AttackStateEnter(); }));
        _mainSM.AddState(Death, new State(onEnter: _ => { DeathStateEnter(); }));
        
        _mainSM.AddTransitionFromAny(Death, _ => isDead());
        _mainSM.AddTwoWayTransition(Search, Attack, _ => isTargetAcquired());
        _mainSM.AddTransition(new TransitionAfter(Activation, Search, ActivationDelay()));
        _mainSM.AddTriggerTransition(RoomActivated, Sleep, Activation);

        _mainSM.SetStartState(Sleep);
        _mainSM.Init();
    }

    // Here's where we should deal with any calculation of hit mitigation.
    private int HitMitigation(int hitDmg)
    {
        return hitDmg >= 0 ? hitDmg : 0; //Just to make sure that we don't get "negative dmg" for any reason
    }
    
    #region State Logic (_mainSM)
    /// <summary>
    /// When the enemy is enabled, but still sleeping. OnEnter
    /// </summary>
    private void SleepStateEnter()
    {
        _mat.color = _enemyData.sleepingColor; // Might already be set, it's just for testing
    }

    /// <summary>
    /// When the player triggers the start of the room. OnEnter
    /// </summary>
    private void ActivationStateEnter()
    {
        _mat.color = _enemyData.activatingColor;
    }

    /// <summary>
    /// Once the activation sequence is over. OnExit
    /// </summary>
    private void ActivationStateExit()
    {
        // TODO Enable the enemy's hurtbox
    }

    /// <summary>
    /// When the enemy starts searching. OnEnter
    /// </summary>
    private void SearchStateEnter()
    {
        _mat.color = _enemyData.searchColor;
    }    
    
    /// <summary>
    /// When the enemy starts attacking. OnEnter
    /// </summary>
    private void AttackStateEnter()
    {
        _mat.color = _enemyData.attackColor;
    }
    
    /// <summary>
    /// When the enemy's HP are down to zero. Play death sequence, give reward and then do a cleanup.
    /// </summary>
    private void DeathStateEnter()
    {
        _mat.color = _enemyData.dyingColor;
    }
    #endregion

    #region Transition Logic (_mainSM)
    private bool isTargetAcquired()
    {
        //TODO : Find a better method name AND add logic to the targeting
        return false;
    }
    
    private bool isDead()
    {
        return _currentHP <= 0;
    }

    private float ActivationDelay()
    {
        // TODO : We should generate the delay based on the starting animation/SFX/effect
        return _enemyData.activationDelay;
    }
    #endregion


    //
    // CODE FROM THE ORIGINAL PROTOTYPE
    //
    // [Header("For testing purposes (shouldn't be edited directly)")]
    // [SerializeField] private Rigidbody2D rb;
    // [SerializeField] private Material mat;
    // [SerializeField] private Vector3 objectivePosition;
    // [SerializeField] private bool isReady;
    // [SerializeField] private int scrapValue;
    // [SerializeField] private int currentHP;
    // [SerializeField] private float baseSpeed;
    //
    // public void InitializeEnemy(SingleEnemyData data)
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     mat = GetComponent<SpriteRenderer>().material;
    //     mat.color = data.baseColor;
    //     currentHP = data.baseHP;
    //     scrapValue = data.scrapValue;
    //     baseSpeed = data.baseSpeed;
    //     UpdateObjectivePosition();
    //
    //     // We currently set the orientation only once, but we could play with that at some point...
    //     transform.Rotate(Vector3.forward * GetDegRotation());
    //     
    //     isReady = true;
    // }
    //
    //
    // public void OnHit()
    // {
    //     currentHP -= 1;
    //     if (currentHP <= 0)
    //     {
    //         GameManager.Instance.OnEnemyKilled(gameObject, scrapValue);
    //         Destroy(gameObject);
    //     }
    //
    //     mat.color /= 2;
    // }
    //
    // private float GetDegRotation()
    // {
    //     var delta = UnityEngine.Vector3.Normalize(objectivePosition - transform.position);
    //     return -(Mathf.Rad2Deg * Mathf.Atan2(delta.x, delta.y));
    // }
    //
    // private void UpdateObjectivePosition()
    // {
    //     objectivePosition = GameManager.Instance.GetCurrentObjectivePosition();
    // }
    //
    // private void FixedUpdate()
    // {
    //     if (isReady)
    //     {
    //         rb.velocity = transform.up * baseSpeed;
    //     }
    // }
}
