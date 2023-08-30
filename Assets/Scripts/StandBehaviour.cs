using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBehaviour : NeoStateMachineBehaviour
{
    protected override void StateUpdate()
    {
        PlayerController.Instance.Stand();
    }
}
