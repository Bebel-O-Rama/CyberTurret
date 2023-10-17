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
    // Enemy Base Variables
    [SerializeField][Min(1)] private int _baseHP;
    [SerializeField][Min(1)] private int _scrapValue;
    [SerializeField][Min(0)] private float _baseSpeed;
    [SerializeField] [Min(0)] private float _activationDelay;

    // TODO Remove temporary settings used for easier testing
    [SerializeField] private Color _sleepingColor;
    [SerializeField] private Color _activatingColor;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _damagedColor;
    [SerializeField] private Color _dyingColor;


    // State Machine & States
    private StateMachine _mainSM;
    private const string Sleep = "Sleep";
    private const string Active = "Active";
    private const string Activation = "Activation";
    private const string Death = "Death";
    private StateMachine _activeSM;
    private const string Search = "Search";
    private const string Attack = "Attack";

    // Current variables
    private Rigidbody2D _rb;
    private Material _mat;
    private bool _isActive;
    private NavMeshAgent _agent;
    
    private int _currentHP;
    private float _currentSpeed;
    private Vector3 _targetPosition;


    private void Awake()
    {
        InitializeEnemyVariables();
        InitializeSM();
    }
    
    private void Update()
    {
        _mainSM.OnLogic();
        
    }

    private void InitializeEnemyVariables()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mat = GetComponent<SpriteRenderer>().material;
        _currentHP = _baseHP;
        _currentSpeed = _baseSpeed;
        
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        
        _isActive = false;
        _mat.color = _sleepingColor;
    }

    private void InitializeSM()
    {
        _mainSM = new StateMachine();
        var activeSM = new StateMachine();

        _mainSM.AddState(Sleep, new State(onEnter: _ => { SleepStateLogic(); }));
        _mainSM.AddState(Activation, new State(
            onEnter: _ => { ActivationStateEnter(); },
            onExit: _ => { ActivationStateExit(); }));
        _mainSM.AddState(Active, activeSM);
        _mainSM.AddState(Death, new State(onEnter: _ => { DeathStateEnter(); }));

        activeSM.AddState(Search, new State(onLogic: _ => { SearchStateLogic(); }));
        activeSM.AddState(Attack, new State(onLogic: _ => { AttackStateLogic(); }));

        _mainSM.AddTransition(Sleep, Activation, _ => _isActive); //TODO ADD A TriggerTransition maybe?
        _mainSM.AddTransition(new TransitionAfter(Activation, Active, _activationDelay));
        _mainSM.AddTransition(Active, Death, _ => isDyingOver());

        activeSM.AddTransitionFromAny(Death, _ => isDead());

        activeSM.AddTransition(Search, Attack, _ => isTargetAcquired());
        activeSM.AddTransition(Attack, Search, _ => !isTargetAcquired());
        
        
        _mainSM.SetStartState("Sleeping");
        _mainSM.Init();
    }

    public void ActivateEnemy()
    {
        _isActive = true;
    }
    
    
    #region States Logic (mainSM)
    /// <summary>
    /// When the player enters the room and the enemy is in "sleep mode". Initial State
    /// TODO Figure out if we should remove the state and init the State Machine only Activation
    /// </summary>
    private void SleepStateLogic()
    {
    }

    /// <summary>
    /// When the player triggers the start of the room. OnEnter
    /// </summary>
    private void ActivationStateEnter()
    {
        _mat.color = _activatingColor;
    }

    /// <summary>
    /// Once the activation sequence is over. OnExit
    /// </summary>
    private void ActivationStateExit()
    {
        _mat.color = _baseColor;
        // TODO Enable the enemy's hurtbox
    }

    /// <summary>
    /// When the enemy's HP are down to zero. Play death sequence, give reward and then do a cleanup.
    /// </summary>
    private void DeathStateEnter()
    {
    }
    #endregion

    #region States Logic (activeSM)
    /// <summary>
    /// When the enemy is searching for a target
    /// </summary>
    private void SearchStateLogic()
    {
    }
    
    /// <summary>
    /// When the enemy have a target to attack
    /// </summary>
    private void AttackStateLogic()
    {
    }
    

    #endregion
    
    #region Transition Logic (mainSM)

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
    
    #region Transition Logic (activeSM)
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
