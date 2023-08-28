using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehaviour : StateMachineBehaviour
{
    Rigidbody body;
    Vector3 originalVelocity;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.transform.parent.GetComponent<Rigidbody>();
        
        originalVelocity =body.velocity;
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body.velocity = Vector3.zero;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body.velocity = originalVelocity;
    }
}
