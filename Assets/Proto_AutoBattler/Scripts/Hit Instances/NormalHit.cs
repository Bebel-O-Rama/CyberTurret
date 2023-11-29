using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HitInstance/NormalHit")]
public class NormalHit : HitInstance
{
    [Min(0)] [SerializeField] public int damageValue;
    
    public override void ProcessHit(UnitInstance targetUnitInstance)
    {
        targetUnitInstance.currentHP -= damageValue;
    }
}
