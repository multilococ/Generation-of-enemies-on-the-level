using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    private void Update()
    {
        Move();   
    }

    private void Move() 
    {
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
    }
}
