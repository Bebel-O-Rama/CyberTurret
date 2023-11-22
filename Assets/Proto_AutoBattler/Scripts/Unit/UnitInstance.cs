using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitInstance : MonoBehaviour
{
    [SerializeField] public UnitType unitType;
    [SerializeField] public UnitData unitData;
    [SerializeField] public UnitInstance currentTarget;

    private void Start()
    {
        UnitTestingManager.Instance.AddUnit(unitType, this);
    }
    
}
