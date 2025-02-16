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
        ChooseWayPoint();
    }

    private void Update()
    {
        ChooseWayPoint();
    }
    
    private void ChooseWayPoint()
    {
        if (_wayPoints.Length  < 1)
        {
            throw new InvalidOperationException();
        }
        else
        {
            if (transform.position == _wayPoints[_currentWayPointIndex].position)
            {
                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
            }

            _mover.SetTarget(_wayPoints[_currentWayPointIndex]);
        }
    }
}
