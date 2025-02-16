using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private int _spawnCount = 100;
    private int _spawnDelay = 2;

    private void Start()
    {
        StartCoroutine(SpawnEnemys(_spawnDelay, _spawnCount));
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        SpawnPoint randomSpawnPoint;

        if (_spawnPoints.Count < 0)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            randomSpawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        }

        return randomSpawnPoint;
    }

    private IEnumerator SpawnEnemys(int delay, int spawnCount)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPoint randomSpawnPoint = GetRandomSpawnPoint();

            randomSpawnPoint.SpawnEnemy();

            yield return waitForSeconds;
        }
    }
}