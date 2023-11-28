using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    // Enemy Base Variables
    [Min(1)] public int baseHP;
    [Min(0)] public float baseSpeed;
    [Min(0)] public float targetingRange;
    [Min(0)] public float hitDamage;
    [Min(1)] public int scrapValue;
    [Min(0)] public float activationDelay;

    // TODO Remove temporary settings used for easier testing
    public Color sleepingColor;
    public Color activatingColor;
    public Color searchColor;
    public Color attackColor;
    public Color damagedColor;
    public Color dyingColor;



    public void Test()
    {
        
    }
}