using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;

[CreateAssetMenu]
public class UnitTargetingModule : ScriptableObject
{
    public Graph targetingModule;

    [Min(0)] public float targetingRangeAround;
    public bool isCheckingCollision;
}
