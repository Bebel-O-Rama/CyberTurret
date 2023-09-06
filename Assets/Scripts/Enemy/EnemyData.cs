using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    // Let's just say that every enemy type names are unique for now... 
    [SerializeField] public List<SingleEnemyData> enemyTypes;

    public static EnemyData Instance => _instance ??= new EnemyData();
    private static EnemyData _instance;

    private EnemyData()
    {
    }

    private void Awake()
    {
        _instance = this;
    }
    
    public SingleEnemyData GetEnemyData(string type)
    {
        var ret = from enemy in enemyTypes where enemy.typeName == type select enemy;
        if (ret.Count() != 1)
        {
            Debug.LogWarning("When fetching the enemy data of type : " + type + ", " + ret.Count() +
                             " element came back");
        }

        return ret.First();
    }
}

[System.Serializable]
public class SingleEnemyData
{
    [SerializeField] public string typeName;
    [SerializeField] public Color baseColor;
    [Min(1)] [SerializeField] public int baseHP = 1;
    [Min(0.1f)] [SerializeField] public float baseSpeed = 0.1f;
    [Min(1)] [SerializeField] public int scrapValue = 1;
}
