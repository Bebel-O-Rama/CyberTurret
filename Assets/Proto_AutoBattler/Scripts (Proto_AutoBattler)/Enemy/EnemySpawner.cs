using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public string key;
    [SerializeField] public bool isActive;

    [Space] [Header("For testing purposes (shouldn't be edited directly)")] [SerializeField]
    public float initialDelay = 0f;

    public List<EnemyDataAndFrequency> enemyDataAndFrequency = new();
    [SerializeField] public float delayBetweenEnemy = 0f;
    [SerializeField] public bool isTypeOrdered = true;
    [SerializeField] public int numberEnemyLeft = 0;
    [SerializeField] public Vector3 spawnerPosition = Vector3.zero;
    [SerializeField] public Vector3 spawnerSize = Vector3.zero;
    [SerializeField] public GameObject enemyPF;
    
    private Transform enemyHolder;

    private void Awake()
    {
        isActive = false;
    }

    private void Start()
    {
        enemyPF = EnemySpawnerManager.Instance.GetEnemyPrefab();
        enemyHolder = FindObjectOfType<EnemyHolder>().transform;
    }

    public void SetWaveData(SingleSpawnerData waveData)
    {
        ClearWaveData();

        initialDelay = waveData.initialDelay;
        SetEnemyDataAndFrequency(waveData.enemyNameAndFrequency);
        delayBetweenEnemy = waveData.delayBetweenEnemy;
        isTypeOrdered = waveData.isTypeOrdered;
        spawnerPosition = transform.position;
        spawnerSize = transform.localScale;
        foreach (var enemyTypes in enemyDataAndFrequency)
        {
            numberEnemyLeft += enemyTypes.frequency;
        }

        InitiateWave();
    }

    public void InitiateWave()
    {
        isActive = true;
        if (initialDelay < Single.Epsilon)
        {
            WaveLoop();
        }
        else
        {
            Invoke("WaveLoop", initialDelay);
        }
    }

    private void WaveLoop()
    {
        int enemyDataIndex = 0;
        if (!isTypeOrdered)
        {
            enemyDataIndex = Random.Range(0, enemyDataAndFrequency.Count);
        }

        SpawnEnemy(enemyDataAndFrequency[enemyDataIndex].enemyData);
        UpdateEnemyDataList(enemyDataIndex);

        if (numberEnemyLeft > 0)
        {
            Invoke("WaveLoop", delayBetweenEnemy);
        }
        else
        {
            isActive = false;
        }
    }

    private void SpawnEnemy(SingleEnemyData enemyData)
    {
        Vector3 enemyPosition = GetEnemyPosition();

        GameObject newEnemy = Instantiate(enemyPF, enemyPosition, Quaternion.identity);
        newEnemy.transform.parent = enemyHolder;
        GameManager.Instance.OnEnemySpawn(newEnemy);
        // newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyData);
    }

    private Vector3 GetEnemyPosition()
    {
        float x = spawnerPosition.x + Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2);
        float y = spawnerPosition.y + Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2);
        return new Vector3(x, y, 0);
    }

    private void UpdateEnemyDataList(int index)
    {
        enemyDataAndFrequency[index].frequency -= 1;
        if (enemyDataAndFrequency[index].frequency <= 0)
        {
            enemyDataAndFrequency.RemoveAt(index);
        }

        numberEnemyLeft -= 1;
    }

    private void SetEnemyDataAndFrequency(List<EnemyNameAndFrequency> enemyWaveData)
    {
        enemyDataAndFrequency.Clear();
        foreach (var type in enemyWaveData)
        {
            enemyDataAndFrequency.Add(new EnemyDataAndFrequency(EnemyData.Instance.GetEnemyData(type.enemyType),
                type.frequency));
        }
    }

    private void ClearWaveData()
    {
        initialDelay = 0f;
        enemyDataAndFrequency.Clear();
        delayBetweenEnemy = 0f;
        numberEnemyLeft = 0;
    }
}
