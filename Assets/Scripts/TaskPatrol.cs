using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System.Linq;

public class TaskPatrol : Node
{
    [SerializeField] bool lookat;
    private Transform[] _waypoints;
    private Transform _transform;
    private int _currentWaypointIndex;
    private float _waitCounter;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _waitTime = 2f;
    private bool _isWaiting;
    [SerializeField] private float offset;
    [SerializeField] bool isCircle;
    public TaskPatrol(Transform transform,Transform[] wayPoints,bool lookat,float speed,float waitTime,bool isCircle)
    {
        _waypoints = wayPoints;
        _transform = transform;
        _speed = speed;
        _waitTime = waitTime;
        this.isCircle = isCircle;

    }
    private void OnDrawGizmos()
    {

        DrawPatrolPos();
        DrawPatrolPath();
        DrawStartPos();
    }
    private void DrawPatrolPos()
    {
        if (_waypoints.Length == 0) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_transform.position, _waypoints[_currentWaypointIndex].position);
        foreach (var waypoint in _waypoints)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(waypoint.position, 1);
        }
    }
    private void DrawStartPos()
    {
        Vector3 offsetPos = Util.CalculatePositionOnPath(_waypoints.Select(x => x.position).ToArray(), offset, isCircle);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(offsetPos, 1);
    }

    private void DrawPatrolPath()
    {
        for (int i = 0; i < _waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
        }
        Gizmos.DrawLine(_waypoints[0].position, _waypoints[_waypoints.Length - 1].position);
    }

    private void Start()
    {

        Vector3 offsetPos = Util.CalculatePositionOnPath(_waypoints.Select(x => x.position).ToArray(), offset, isCircle, out int pathIndex);
        _currentWaypointIndex = pathIndex < _waypoints.Length - 1 ? pathIndex + 1 : pathIndex;
        _transform.position = offsetPos;
    }

    public override NodeState Evaluate()
    {
        Transform wp = _waypoints[_currentWaypointIndex];
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter <= _waitTime)
            {
                return state = NodeState.RUNNING;
            }
            _isWaiting = false;
        }
        if (Vector3.Distance(wp.position, _transform.position) <= 0.01f)
        {
            _transform.position = wp.position;
            if (!isCircle && _currentWaypointIndex == _waypoints.Length - 1)
            {
                System.Array.Reverse(_waypoints);
                _currentWaypointIndex++;
            }
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;


            _isWaiting = true;
            _waitCounter = 0;
        }
        else
        {
            if (lookat)
                _transform.LookAt(wp.position);
            _transform.position = Vector3.MoveTowards(_transform.position, wp.position, _speed * Time.deltaTime);
        }
        state = NodeState.RUNNING;
        return state;
    }
}
