using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class TaskGotoTarget : Node
{
    private Transform _transform;
    private float _speed=10f;

    public TaskGotoTarget(Transform transform,float speed)
    {
        _transform = transform;
        _speed = speed;
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
     
        if (Vector3.Distance(target.position, _transform.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _transform.LookAt(target.position,Vector3.up);
        }
        state = NodeState.RUNNING;
        Debug.Log(state);
        return state;
    }
}
