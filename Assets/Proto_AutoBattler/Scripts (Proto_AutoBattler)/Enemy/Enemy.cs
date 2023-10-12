using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    // Enemy Base Variables
    [SerializeField][Min(1)] private int _baseHP;
    [SerializeField][Min(1)] private int _scrapValue;
    [SerializeField][Min(0)] private float _baseSpeed;

    // TODO Remove temporary settings used for easier testing
    [SerializeField] [Min(0)] private float _activationDelay;
    [SerializeField] private Color _sleepingColor;
    [SerializeField] private Color _activatingColor;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _damagedColor;
    [SerializeField] private Color _dyingColor;


    // State Machine & States
    private StateMachine _stateMachine;
    private const string Sleeping = "Sleeping";
    private const string Activating = "Activating";
    private const string Search = "Search";
    private const string Attack = "Attack";
    private const string Dying = "Dying";
    private const string CleanUp = "CleanUp";

    // Current variables
    private Rigidbody2D _rb;
    private Material _mat;
    private bool _isActive;
    private bool _isActivationSequenceOver;
    
    private int _currentHP;
    private float _currentSpeed;
    private Vector3 _targetPosition;


    private void Awake()
    {
        InitializeEnemyVariables();
        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnLogic();
    }

    private void InitializeEnemyVariables()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mat = GetComponent<SpriteRenderer>().material;
        _currentHP = _baseHP;
        _currentSpeed = _baseSpeed;
        
        _isActive = false;
        _isActivationSequenceOver = true; // TODO Remove that
        _mat.color = _sleepingColor;
    }
    
    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();
        
        _stateMachine.AddState(Sleeping, new State(onLogic: _ => { SleepingLogic(); }));
        _stateMachine.AddState(Activating, new State(onLogic: _ => { ActivatingLogic(); }));
        _stateMachine.AddState(Search, new State(onLogic: _ => { SearchLogic(); }));
        _stateMachine.AddState(Attack, new State(onLogic: _ => { AttackLogic(); }));
        _stateMachine.AddState(Dying, new State(onLogic: _ => { DyingLogic(); }));
        _stateMachine.AddState(CleanUp, new State(onLogic: _ => { CleanUpLogic(); }));
        
        _stateMachine.AddTransition(Sleeping, Activating, _ => _isActive);
        _stateMachine.AddTransition(Activating, Search, _ => _isActivationSequenceOver);
        _stateMachine.AddTransition(Search, Attack, _ => isTargetAcquired());
        _stateMachine.AddTransition(Attack, Search, _ => !isTargetAcquired());
        _stateMachine.AddTransition(Dying, CleanUp, _ => isDyingOver());
        
        _stateMachine.AddTransitionFromAny(Dying, _ => isDead());
        
        _stateMachine.SetStartState("Sleeping");
        _stateMachine.Init();
    }

    public void ActivateEnemy()
    {
        _isActive = true;
    }

    private void ActivationSequenceOver()
    {
        _mat.color = _baseColor;
        _isActivationSequenceOver = true;
    }
    
    #region States Logic
    /// <summary>
    /// When the player enters the room and the enemy is in "sleep mode". Initial State
    /// TODO Figure out if we should remove the state and init the State Machine only Activation
    /// </summary>
    private void SleepingLogic()
    {
    }

    /// <summary>
    /// When the player triggers the start of the room. 
    /// </summary>
    private void ActivatingLogic()
    {
        _mat.color = _activatingColor;
        Invoke("ActivationSequenceOver", _activationDelay);
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
    
    /// <summary>
    /// Once the enemy is death and we have nothing left to do with it, clean it up! 
    /// </summary>
    private void CleanUpLogic()
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
