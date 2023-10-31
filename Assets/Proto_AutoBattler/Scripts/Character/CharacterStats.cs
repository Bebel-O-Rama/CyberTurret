using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character Stats", order = 0)]
public class CharacterStats : ScriptableObject
{
    [Min(0)] public int baseHP = 1;
    [Min(0)] public float baseSpeed = 1;
    [Min(0)] public float hitDamage = 1;
    [Min(0)] public int scrapValue = 0;

    [Header("TODO : Remove these (temporary stuff for testing)")]
    public GameObject instantiationPrefab;
    [Min(0)] public float activationDelay;
    public Color sleepingColor;
    public Color activatingColor;
    public Color searchColor;
    public Color attackColor;
    public Color damagedColor;
    public Color dyingColor;

    public void ApplyStatsModifiers(CharacterStatModifiers mod)
    {
        baseHP = Mathf.RoundToInt(mod.healthMultiplier * baseHP);
        baseSpeed *= mod.speedMultiplier;
        hitDamage *= mod.damageMultipler;
    }
}
