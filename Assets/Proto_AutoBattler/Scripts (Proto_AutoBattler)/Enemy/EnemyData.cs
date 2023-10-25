using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    // Enemy Base Variables
    [Min(1)] public int baseHP;
    [Min(0)] public float baseSpeed;
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

    public void SpawnEnemy(GameObject pf, Vector3 position, float hpMod, float speedMod, float dmgMod)
    {
        if (!Mathf.Approximately(hpMod, 1f) || !Mathf.Approximately(speedMod, 1f) || !Mathf.Approximately(dmgMod, 1f))
            ApplyStatsModifiers(hpMod, speedMod, dmgMod);
        var enemy = Instantiate(pf, position, Quaternion.identity);
        enemy.GetComponent<Enemy>().OnInstantiation(this);
    }

    private void ApplyStatsModifiers(float hpMod, float speedMod, float dmgMod)
    {
        baseHP = Mathf.RoundToInt(hpMod * baseHP);
        baseSpeed *= speedMod;
        hitDamage *= dmgMod;
    }
}