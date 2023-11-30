using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HitInstance/HealingHit")]
public class HealingHit : HitType
{
    [SerializeField] public bool canOverHeal = false;
    
    public override void ProcessHit(UnitInstance targetUnitInstance, HitData hitData)
    {
        targetUnitInstance.currentHP += hitData.attackDamage;
        if (!canOverHeal && targetUnitInstance.currentHP > targetUnitInstance.maxHP)
            targetUnitInstance.currentHP = targetUnitInstance.maxHP;
    }
}
