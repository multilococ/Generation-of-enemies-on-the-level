using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Enemy _enemyPrefab;

    private ObjectPool<Enemy> _enemyPool;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _enemyPool = CreatePool();
    }

    public void SpawnEnemy()
    {
        Enemy enemy = _enemyPool.Get();

        enemy.Init(transform.position,_target);
        enemy.Died += ReleaseEnemy;
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

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.Died -= ReleaseEnemy;
        _enemyPool.Release(enemy);
    }
}