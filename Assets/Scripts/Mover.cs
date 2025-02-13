using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _body;
    
    [SerializeField] private float _moveSpeed = 10f;

    private Vector3 _moveDerection = Vector3.zero;

    private void Update()
    {
        Move(_moveDerection);
    }

    public void SetDerecrtion(Vector3 derection)
    {
        derection = derection.normalized;
        _moveDerection = derection;
        _body.transform.forward = derection;
    }

    private void Move(Vector3 derection)
    {
        transform.Translate(derection * _moveSpeed * Time.deltaTime);
    }
}