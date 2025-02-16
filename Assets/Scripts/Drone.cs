using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Drone : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    private Mover _mover;

    private int _currentWayPointIndex = 0;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        SetWay();
    }

    private void Update()
    {
        SetWay();
    }

    private void SetWay()
    {
        if (_wayPoints.Length > 1)
        {
            if (transform.position == _wayPoints[_currentWayPointIndex].position)
            {
                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
            }

            _mover.SetTarget(_wayPoints[_currentWayPointIndex]);
        }
        else 
        {
            throw new InvalidOperationException();
        }
    }
}
