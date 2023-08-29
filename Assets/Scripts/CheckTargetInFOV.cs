using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckTargetInFOV : Node
{
    private Transform _transform;
    private LayerMask _layermask = 1 << 6;
    private float _fovRange = 3;

    public CheckTargetInFOV(Transform transform, float range, LayerMask layerMask)
    {
        _transform = transform;
        _layermask = layerMask;
        _fovRange = range;
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] collider = Physics.OverlapSphere(_transform.position, _fovRange, _layermask);
            if (collider.Length > 0)
            {
                Debug.Log(collider[0]);
                parent.parent.SetData("target", collider[0].transform);
                state = NodeState.SUCCEED;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCEED;
        return state;
    }
}

