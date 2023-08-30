using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class GuardBT : BehaviourTree.Tree
{
    [SerializeField] Transform[] wayPoint;
    [SerializeField] LayerMask maskTarget;
    [SerializeField] private float gotoSpeed = 4f;
    [SerializeField] private float rangeCheck = 10f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeCheck);
    }
    protected override Node SetUpTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
             new CheckTargetInFOV(transform,rangeCheck,maskTarget),
             new TaskGotoTarget(transform,gotoSpeed,15f)
            }),
            new TaskPatrol(transform,wayPoint,true,5,2,true),
        });
        Debug.Log(root.Evaluate());
            return root;
    }
}
