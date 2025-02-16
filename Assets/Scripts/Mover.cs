using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _body;
    
    [SerializeField] private float _moveSpeed = 10f;

    private Transform _target;

    private void Update()
    {
        Move();
        LookAtTarget(_target);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void LookAtTarget(Transform target) 
    {
        transform.LookAt(target);
    }

    private void Move()
    {
         transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
    }
}