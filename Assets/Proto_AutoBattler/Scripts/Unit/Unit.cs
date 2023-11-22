using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour
{
    [SerializeField] public UnitType unitType;
    [SerializeField] public UnitData unitData;

    private void Start()
    {
        UnitTestingManager.Instance.AddUnit(unitType, this);
    }
    
}
