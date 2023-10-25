using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomEnemy
{
    public EnemyData enemyData;
    public Vector3 initialPosition;
    [Header("Other modifiers")]
    [Min(0f)] public float healthMultiplier = 1;
    [Min(0f)] public float speedMultiplier = 1;
    [Min(0f)] public float damageMultipler = 1;
}
