using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5;

    private Mover _mover;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Init(Vector3 position, Vector3 derection) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _mover.SetDerecrtion(derection);
        StartCoroutine(DeathWithDelay(_lifeTime));
    }

    private IEnumerator DeathWithDelay(float delay) 
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
    
        Died?.Invoke(this);
    }
}