using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    public int attackDamage;

    public HitData(UnitInstance unitInstance)
    {
        attackDamage = unitInstance.hitDamage;
    }
}
