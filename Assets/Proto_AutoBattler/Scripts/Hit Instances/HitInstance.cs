using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitInstance : ScriptableObject
{
    public abstract bool ProcessHit(UnitInstance targetUnitInstance);
    // Check to add a StatusEffect type we could use. Uncomment this method when it happens (I don't want to return random null for nothing)
    // public virtual StatusEffect addStatusEffect(UnitInstance targetUnitInstance) { return null; }
}
