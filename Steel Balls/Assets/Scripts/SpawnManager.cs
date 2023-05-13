using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    public float heavyBallSpawnRate;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnEnemyBoss(waveNumber);
            SpawnPowerUp();
        }
    }

    // Создать случайную точку для спавна врага
    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Создать волну врагов
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[SpawnEnemyIndex()], GenerateSpawnPosition(), enemyPrefabs[SpawnEnemyIndex()].transform.rotation);
        }
    }

    // Создать босса на каждой пятой волне
    void SpawnEnemyBoss(int wave)
    {
        if (wave % 5 == 0)
        {
            Instantiate(enemyPrefabs[2], (GenerateSpawnPosition() + new Vector3(0, 0.35f, 0)), enemyPrefabs[2].transform.rotation);
        }
    }

    int SpawnEnemyIndex()
    {
        float num = Random.Range(0f, 1f);
        int index;
        if (num <= heavyBallSpawnRate) index = 1;
        else index = 0;
        return index;
    }

    void SpawnPowerUp()
    {
        float num = Random.Range(0f, 1f);
        int index;
        if (num <= 0.33f)
        {
            index = 0;
        }
        else if (num > 0.33f && num <= 0.66f)
        {
            index = 1;
        }
        else
        {
            index = 2;
        } 
        Instantiate(powerUpPrefabs[index], GenerateSpawnPosition(), powerUpPrefabs[index].transform.rotation);
    }
}
