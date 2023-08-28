using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] bool lookat;
    [SerializeField] Transform[] waypoints;
    private int _currentWaypointIndex;
    private float _waitCounter;
    [SerializeField] private int _speed=5;
    [SerializeField] private float _waitTime=2f;
    private bool _isWaiting;
    private float offset;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, waypoints[_currentWaypointIndex].position);
    }
    private void Start()
    {
        transform.position = waypoints[0].position;
    }
    private void Update()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter <= _waitTime)
            {
                return;
            }
            _isWaiting = false;
        }
        if (Vector3.Distance(wp.position, transform.position) <= 0.01f)
        {
            transform.position = wp.position;
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            _isWaiting = true;
            _waitCounter = 0;
        }
        else
        {
            if(lookat)
            transform.LookAt(wp.position);
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed*Time.deltaTime);
        }
    }
}
