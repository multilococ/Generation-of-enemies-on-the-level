using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _lifeTime = 5;

    private Vector3 _moveDerection;

    public event Action<Enemy> OnDied;

    private void Update()
    {
        Move(_moveDerection);   
    }

    public void Init(Vector3 position, Vector3 derection) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _moveDerection = derection;
        StartCoroutine(DeathWithDelay(_lifeTime));
    }

    private void Move(Vector3 derection) 
    {
        transform.Translate(derection * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator DeathWithDelay(float delay) 
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
    
        OnDied?.Invoke(this);
    }
}
