using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] bool lookat;
    [SerializeField] Transform[] waypoints;
    private int _currentWaypointIndex;
    private float _waitCounter;
    [SerializeField] private int _speed = 5;
    [SerializeField] private float _waitTime = 2f;
    private bool _isWaiting;
    [SerializeField] private float offset;
    [SerializeField] bool isCircle;
    private void OnDrawGizmos()
    {

        DrawPatrolPos();
        DrawPatrolPath();
        DrawStartPos();
    }
    private void DrawPatrolPos()
    {
        if (waypoints.Length == 0) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, waypoints[_currentWaypointIndex].position);
        foreach (var waypoint in waypoints)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(waypoint.position, 1);
        }
    }
    private void DrawStartPos()
    {
        Vector3 offsetPos = Util.CalculatePositionOnPath(waypoints.Select(x => x.position).ToArray(), offset, isCircle);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(offsetPos, 1);
    }

    private void DrawPatrolPath()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
        Gizmos.DrawLine(waypoints[0].position, waypoints[waypoints.Length - 1].position);
    }

    private void Start()
    {

        Vector3 offsetPos = Util.CalculatePositionOnPath(waypoints.Select(x => x.position).ToArray(), offset, isCircle, out int pathIndex);
        _currentWaypointIndex = pathIndex < waypoints.Length - 1 ? pathIndex + 1 : pathIndex;
        transform.position = offsetPos;
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
            if (!isCircle&&_currentWaypointIndex==waypoints.Length-1)
            {
                System.Array.Reverse(waypoints);
                _currentWaypointIndex++;
            }
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
           
              
            _isWaiting = true;
            _waitCounter = 0;
        }
        else
        {
            if (lookat)
                transform.LookAt(wp.position);
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
        }
    }
   
}
