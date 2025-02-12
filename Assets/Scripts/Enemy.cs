using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _lifeTime = 5;

    public event Action<Enemy> OnDied;

    private void Update()
    {
        Move();   
    }

    public void Init(Vector3 position, Vector3 rotation) 
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
        StartCoroutine(DeathWithDelay(_lifeTime));
    }

    private void Move() 
    {
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator DeathWithDelay(float delay) 
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
    
        OnDied?.Invoke(this);
    }
}
