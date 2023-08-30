using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class TaskGotoTarget : Node
{
    private float _offset = 10f;
    private Transform _transform;
    private float _speed=10f;
    private Animator _animator;
    private Rigidbody _rig;
   
    float _waitTime = 1;
    private float _waitCounter;

    public TaskGotoTarget(Transform transform,float speed)
    {
        _transform = transform;
        _speed = speed;
        _animator = transform.GetComponent<Animator>();
        _rig = transform.GetComponent<Rigidbody>();
    }
    public TaskGotoTarget(Transform transform, float speed, float offset)
    {
        _transform = transform;
        _speed = speed;
        _animator = transform.GetComponent<Animator>();
        _rig = transform.GetComponent<Rigidbody>();
        _offset = offset;
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        PathFixTemp();

        if (Mathf.Abs(_transform.position.x - target.position.x) > _offset)
        {
            //need return state instead of return
            _animator.SetBool("IsWalking", false);
            _waitCounter += Time.deltaTime;
            if (_waitCounter <= _waitTime)
            {
                Util.LookAtLerp(_transform, target, 3);
                return state = NodeState.RUNNING;
            }
            _waitCounter = 0;



            var horizontalVelocity = (_transform.position.x > target.position.x ? -1 : 1) * _speed;
            _rig.velocity = new Vector2(horizontalVelocity, _rig.velocity.y);

            //_transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsWalking", false);

        }
        else
        {
            _animator.SetBool("IsRunning", false);
            _rig.velocity = new Vector3(0, _rig.velocity.y, _rig.velocity.z);
        }


        state = NodeState.RUNNING;
        Debug.Log(state);
        return state;
    }

    private void PathFixTemp()
    {
        var v = _transform.position;
        v.z = 0;
        _transform.position = v;
    }

}
