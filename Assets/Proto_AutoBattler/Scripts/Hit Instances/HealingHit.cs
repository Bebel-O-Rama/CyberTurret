using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HitInstance/HealingHit")]
public class HealingHit : HitInstance
{
    [Min(0)] [SerializeField] public int healingStrength;
    [SerializeField] public bool canOverHeal = false;
    
    public override bool ProcessHit(UnitInstance targetUnitInstance)
    {
        targetUnitInstance.currentHP += healingStrength;
        if (!canOverHeal && targetUnitInstance.currentHP > targetUnitInstance.maxHP)
            targetUnitInstance.currentHP = targetUnitInstance.maxHP;
        return true;
    }
}
