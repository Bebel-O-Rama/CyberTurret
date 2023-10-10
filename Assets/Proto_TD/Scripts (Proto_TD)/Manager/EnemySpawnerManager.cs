using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<WaveEnemySpawnerData> enemySpawnersData = new();
    private Dictionary<string, EnemySpawner> enemySpawners = new();

    [SerializeField] public GameObject enemyPF;
    public static EnemySpawnerManager Instance => _instance ??= new EnemySpawnerManager();

    private static EnemySpawnerManager _instance;

    private EnemySpawnerManager()
    {
    }

    private void Awake()
    {
        _instance = this;
        SetEnemySpawners();
    }

    public void InitiateNewWave(int waveNumber)
    {
        WaveEnemySpawnerData newWave = enemySpawnersData[waveNumber];
        foreach (var spawnerData in newWave.waveEnemySpawnerData)
        {
            enemySpawners[spawnerData.spawnerKey].SetWaveData(spawnerData);
        }
    }

    public string GetWaveName(int waveNumber)
    {
        return enemySpawnersData[waveNumber].waveName;
    }
    public int GetLastWaveNumber()
    {
        if (enemySpawnersData == null)
            return 0;
        return enemySpawnersData.Count();
    }
    
    public int GetTotalEnemyCountForWave(int waveNumber)
    {
        var enemyCount = 0;
        if (waveNumber >= enemySpawnersData.Count)
            return enemyCount;
        WaveEnemySpawnerData waveData = enemySpawnersData[waveNumber];
        foreach (var spawner in waveData.waveEnemySpawnerData)
        {
            foreach (var enemyBundle in spawner.enemyNameAndFrequency)
            {
                enemyCount += enemyBundle.frequency;
            }
        }

        return enemyCount;
    }
    
    public void SetEnemySpawners()
    {
        enemySpawners.Clear();
        foreach (var spawner in FindObjectsOfType<EnemySpawner>())
        {
            EnemySpawner item;
            if (enemySpawners.TryGetValue(spawner.key, out item))
            {
                Debug.LogWarning("There are more than one EnemySpawner using the key " + spawner.key);
            }

            enemySpawners.Add(spawner.key, spawner);
        }
    }

    public GameObject GetEnemyPrefab()
    {
        return enemyPF;
    }
}

[System.Serializable]
public class WaveEnemySpawnerData
{
    [SerializeField] public string waveName = "";
    [SerializeField] public List<SingleSpawnerData> waveEnemySpawnerData = new();
}

[System.Serializable]
public class SingleSpawnerData
{
    [SerializeField] public string spawnerKey = "";
    [SerializeField] public float initialDelay = 0f;
    [SerializeField] public List<EnemyNameAndFrequency> enemyNameAndFrequency = new();
    [SerializeField] public float delayBetweenEnemy = 0f;
    [SerializeField] public bool isTypeOrdered = true;
}

[System.Serializable]
public class EnemyNameAndFrequency
{
    [SerializeField] public string enemyType = "";
    [SerializeField] public int frequency = 1;
}

[System.Serializable]
public class EnemyDataAndFrequency
{
    [SerializeField] public SingleEnemyData enemyData = new();
    [SerializeField] public int frequency = 1;

    public EnemyDataAndFrequency(SingleEnemyData enemyData, int frequency)
    {
        this.enemyData = enemyData;
        this.frequency = frequency;
    }
}
