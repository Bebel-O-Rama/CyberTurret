using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitTestingManager : MonoBehaviour
{
    private Dictionary<UnitType, List<Unit>> spawnedUnits;

    [SerializeField] public List<Unit> tempUnitInRoom;

    public static UnitTestingManager Instance => _instance ??= new UnitTestingManager();
    private static UnitTestingManager _instance;

    private UnitTestingManager()
    {
    }

    private void Awake()
    {
        spawnedUnits = new Dictionary<UnitType, List<Unit>>();
        tempUnitInRoom = new List<Unit>();
        _instance = this;
    }

    public void AddUnit(UnitType type, Unit unit)
    {
        if (spawnedUnits.ContainsKey(type))
        {
            spawnedUnits[type].Add(unit);
        }
        else
            spawnedUnits[type] = new List<Unit> { unit };
        
        //TEMP STUFF
        tempUnitInRoom.Add(unit);
    }

    public void RemoveUnit(UnitType type, Unit unit)
    {
        spawnedUnits[type].Remove(unit);
        if (!spawnedUnits[type].Any())
            spawnedUnits.Remove(type);
        
        //TEMP STUFF
        tempUnitInRoom.Remove(unit);
    }

    public List<Unit> GetOpposingUnits(UnitType type)
    {
        List<Unit> opposingUnits = new List<Unit>();
        foreach (var t in type.opposingTypes)
        {
            if (spawnedUnits.ContainsKey(t))
                opposingUnits.AddRange(spawnedUnits[t]);
        }
        return opposingUnits;
    }
}