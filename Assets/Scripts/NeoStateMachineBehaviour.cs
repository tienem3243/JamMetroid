using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoStateMachineBehaviour : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetLayerWeight(layerIndex) == 0) return;
        StateEnter();

    }



    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetLayerWeight(layerIndex) == 0) return;
        StateUpdate();
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetLayerWeight(layerIndex) == 0) return;
        StateExit();
    }
    protected virtual void StateUpdate()
    {
        
    }
    protected virtual void StateEnter()
    {

    }
    protected virtual void StateExit()
    {

    }
}
