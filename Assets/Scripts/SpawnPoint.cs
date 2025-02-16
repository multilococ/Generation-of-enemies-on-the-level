using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private EnemyPool _enemyPool;

    private void Awake()
    {
        _enemyPool = GetComponent<EnemyPool>();
    }

    public void SpawnEnemy() 
    {
        _enemyPool.SpawnEnemyWithTarget(_target);
    }
}