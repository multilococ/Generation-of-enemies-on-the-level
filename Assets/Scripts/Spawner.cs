using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private ObjectPool<Enemy> _enemyPool;
    
    private OrientationRandomazer _orientationRandomazer;

    private int _spawnCount = 100;
    private int _spawnDelay = 2;
    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;
    private void Awake()
    {
        _enemyPool = CreatePool();
        _orientationRandomazer = new OrientationRandomazer();
        StartCoroutine(SpawnEnemys(_spawnDelay,_spawnCount));
    }

    private ObjectPool<Enemy> CreatePool()
    {
        return new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnGet: (enemy) => GetObjectFromPool(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void GetObjectFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void GetEnemy()
    {
        Enemy enemy = _enemyPool.Get();
        enemy.Init(_orientationRandomazer.GetRandomSpawnPointPosition(_spawnPoints),_orientationRandomazer.GetRandomDerection());
        enemy.OnDied += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.OnDied -= ReleaseEnemy;
        _enemyPool.Release(enemy);
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
