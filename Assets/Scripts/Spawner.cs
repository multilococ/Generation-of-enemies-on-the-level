using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private ObjectPool<Enemy> _enemyPool;

    private int _spawnCount = 100;
    private int _spawnDelay = 2;
    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _enemyPool = CreatePool();
        StartCoroutine(SpawnEnemys(_spawnDelay, _spawnCount));
    }

    private ObjectPool<Enemy> CreatePool()
    {
        return new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnGet: (enemy) => SetEnemyActive(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void SetEnemyActive(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void GetEnemy()
    {
        Enemy enemy = _enemyPool.Get();

        enemy.Init(GetRandomSpawnPosition(), GetRandomDerection());
        enemy.Died += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.Died -= ReleaseEnemy;
        _enemyPool.Release(enemy);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        if (_spawnPoints.Count > 0)
        {
            spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
        }

        return spawnPosition;
    }

    private Vector3 GetRandomDerection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    private IEnumerator SpawnEnemys(int delay, int spawnCount)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        for (int i = 0; i < spawnCount; i++)
        {
            GetEnemy();

            yield return waitForSeconds;
        }
    }
}
