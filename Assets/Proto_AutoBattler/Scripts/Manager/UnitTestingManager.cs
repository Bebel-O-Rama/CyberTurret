using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitTestingManager : MonoBehaviour
{
    private Dictionary<UnitType, List<UnitInstance>> spawnedUnits;

    [SerializeField] public List<UnitInstance> tempUnitInRoom;

    public static UnitTestingManager Instance => _instance ??= new UnitTestingManager();
    private static UnitTestingManager _instance;

    private UnitTestingManager()
    {
    }

    private void Awake()
    {
        spawnedUnits = new Dictionary<UnitType, List<UnitInstance>>();
        tempUnitInRoom = new List<UnitInstance>();
        _instance = this;
    }

    public void AddUnit(UnitType type, UnitInstance unit)
    {
        if (spawnedUnits.ContainsKey(type))
        {
            spawnedUnits[type].Add(unit);
        }
        else
            spawnedUnits[type] = new List<UnitInstance> { unit };
        
        //TEMP STUFF
        tempUnitInRoom.Add(unit);
    }

    public void RemoveUnit(UnitType type, UnitInstance unit)
    {
        spawnedUnits[type].Remove(unit);
        if (!spawnedUnits[type].Any())
            spawnedUnits.Remove(type);
        
        //TEMP STUFF
        tempUnitInRoom.Remove(unit);
    }

    public List<UnitInstance> GetOpposingUnits(UnitType type)
    {
        List<UnitInstance> opposingUnits = new List<UnitInstance>();
        foreach (var t in type.opposingTypes)
        {
            if (spawnedUnits.ContainsKey(t))
                opposingUnits.AddRange(spawnedUnits[t]);
        }
        return opposingUnits;
    }
}