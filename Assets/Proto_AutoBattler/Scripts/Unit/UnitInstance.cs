using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitInstance : MonoBehaviour
{
    [SerializeField] public UnitType unitType;
    [SerializeField] public UnitData unitData;
    
    public Queue<HitInstance> hitInstances;
    
    [Header("DON'T TOUCH ANYTHING UNDER THIS HEADER (It's just for testing), will be either removed or private")] 
    
    [SerializeField] public UnitInstance currentTarget;

    [SerializeField] public int currentHP;
    

    [SerializeField] [Min(1)] public int maxHP;
    [SerializeField] [Min(0)] public float baseSpeed;
    [SerializeField] [Min(0)] public float targetingRange;
    [SerializeField] [Min(0)] public float hitDamage;
    [SerializeField] [Min(1)] public int scrapValue;
    [SerializeField] [Min(0)] public float activationDelay;

    [SerializeField] public Color sleepingColor;
    [SerializeField] public Color activatingColor;
    [SerializeField] public Color searchColor;
    [SerializeField] public Color attackColor;
    [SerializeField] public Color damagedColor;
    [SerializeField] public Color dyingColor;

    private void Awake()
    {
        SetUnitData();
    }

    private void Start()
    {
        if (SetUnitData())
        {
            UnitTestingManager.Instance.AddUnit(unitType, this);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void AddHitInstance(HitInstance hitInstance)
    {
        hitInstances.Enqueue(hitInstance);
    }

    public void KillUnit()
    {
        UnitTestingManager.Instance.RemoveUnit(unitType, this);
        Destroy(gameObject);
    }
    
    private bool SetUnitData()
    {
        if (unitData == null)
        {
            Debug.LogWarning("The unitData is missing for the GameObject " + transform.name);
            return false;
        }

        maxHP = unitData.baseHP;
        baseSpeed = unitData.baseSpeed;
        targetingRange = unitData.targetingRange;
        hitDamage = unitData.hitDamage;
        scrapValue = unitData.scrapValue;
        activationDelay = unitData.activationDelay;

        sleepingColor = unitData.sleepingColor;
        activatingColor = unitData.activatingColor;
        searchColor = unitData.searchColor;
        attackColor = unitData.attackColor;
        damagedColor = unitData.damagedColor;
        dyingColor = unitData.dyingColor;

        hitInstances = new Queue<HitInstance>();
        
        return true;
    }
}