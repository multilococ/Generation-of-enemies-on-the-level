using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Mover _mover;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Init(Vector3 position, Transform target) 
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _mover.SetTarget(target);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Drone>() != null) 
        {
            Died?.Invoke(this);
        }
    }
}