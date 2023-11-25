using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitInstance : MonoBehaviour
{
    [SerializeField] public UnitType unitType;
    [SerializeField] public UnitData unitData;
    
    [Header("DON'T TOUCH ANYTHING UNDER THIS HEADER")]
    [SerializeField] public UnitInstance currentTarget;

    [SerializeField] public Seeker seeker; // Can't bind it to a BB without getting it here
    
    [SerializeField] [Min(1)] public int baseHP;
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
    
    private bool SetUnitData()
    {
        if (unitData == null)
        {
            Debug.LogWarning("The unitData is missing for the GameObject " + transform.name);
            return false;
        }
        baseHP = unitData.baseHP;
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

        seeker = GetComponent<Seeker>();
        
        return true;
    }
}
