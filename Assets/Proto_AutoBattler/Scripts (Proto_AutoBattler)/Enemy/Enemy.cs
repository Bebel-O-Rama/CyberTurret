using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
public class Enemy : MonoBehaviour
{
    // Enemy Base Variables
    [SerializeField] private int _baseHP = 3;

    // State Machine & States
    private StateMachine _stateMachine;
    private const string Sleeping = "Sleeping";
    private const string Activating = "Activating";
    private const string CleanUp = "CleanUp";
    private const string Search = "Search";
    private const string Attack = "Attack";
    private const string Dying = "Dying";

    // Current variables
    private bool _isRoomActive;
    private bool _isActivationOver;

    private int _currentHP;

    private void Awake()
    {
        InitializeEnemyVariables();
        InitializeStateMachine();
    }

    private void InitializeEnemyVariables()
    {
        _isRoomActive = false;
        _isActivationOver = false;
        _currentHP = _baseHP;
    }
    
    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();
        
        _stateMachine.AddState(Sleeping, new State(onLogic: _ => { SleepingLogic(); }));
        _stateMachine.AddState(Activating, new State(onLogic: _ => { ActivatingLogic(); }));
        _stateMachine.AddState(CleanUp, new State(onLogic: _ => { CleanUpLogic(); }));
        _stateMachine.AddState(Search, new State(onLogic: _ => { SearchLogic(); }));
        _stateMachine.AddState(Attack, new State(onLogic: _ => { AttackLogic(); }));
        _stateMachine.AddState(Dying, new State(onLogic: _ => { DyingLogic(); }));
        
        _stateMachine.AddTransition(Sleeping, Activating, _ => _isRoomActive);
        _stateMachine.AddTransition(Activating, Search, _ => _isActivationOver);
        _stateMachine.AddTransition(Search, Attack, _ => isTargetAcquired());
        _stateMachine.AddTransition(Attack, Search, _ => !isTargetAcquired());
        _stateMachine.AddTransition(Dying, CleanUp, _ => isDyingOver());
        
        _stateMachine.AddTransitionFromAny(Dying, _ => isDead());
    }
    
    #region States Logic
    /// <summary>
    /// When the player enters the room and the enemy is in "sleep mode". Initial State
    /// </summary>
    private void SleepingLogic()
    {
    }

    /// <summary>
    /// When the player triggers the start of the room. 
    /// </summary>
    private void ActivatingLogic()
    {
    }
    
    /// <summary>
    /// Once the enemy is death and we have nothing left to do with it, clean it up! 
    /// </summary>
    private void CleanUpLogic()
    {
    }

    /// <summary>
    /// When the enemy is searching for a target
    /// </summary>
    private void SearchLogic()
    {
    }
    
    /// <summary>
    /// When the enemy have a target to attack
    /// </summary>
    private void AttackLogic()
    {
    }
    
    /// <summary>
    /// When the enemy's HP are down to zero. Any reward and feedback will be done here.
    /// </summary>
    private void DyingLogic()
    {
    }
    #endregion
    
    #region Transition Logic

    private bool isTargetAcquired()
    {
        //TODO : Find a better method name AND add logic to the targeting
        return false;
    }
    
    private bool isDead()
    {
        return _currentHP <= 0;
    }

    private bool isDyingOver()
    {
        //TODO : Find a better method name AND add logic to the dying state
        return true;
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
