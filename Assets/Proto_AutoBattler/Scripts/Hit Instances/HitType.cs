using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitType : ScriptableObject
{
    public abstract void ProcessHit(UnitInstance targetUnitInstance, HitData hitData);
    public virtual bool IsUnitDead(UnitInstance targetUnitInstance)
    {
        return targetUnitInstance.currentHP <= 0;
    }
    
    // Check to add a StatusEffect type we could use. Uncomment this method when it happens (I don't want to return random null for nothing)
    // public virtual StatusEffect addStatusEffect(UnitInstance targetUnitInstance) { return null; }
}
