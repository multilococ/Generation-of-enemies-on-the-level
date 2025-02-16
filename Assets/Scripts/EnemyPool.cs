using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private ObjectPool<Enemy> _pool;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _pool = CreatePool();
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

    public void SpawnEnemyWithTarget(Transform target)
    {
        Enemy enemy = _pool.Get();

        enemy.Init(transform.position, target);
        enemy.Died += ReleaseEnemy;
    }

    private void SetEnemyActive(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.Died -= ReleaseEnemy;
        _pool.Release(enemy);
    }
}
