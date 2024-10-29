using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : State
{
    public StateRun(StateMachineV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.red;
        machine.bodySprite.color = Color.red;
        machine.currentSpeed = machine.runSpeed;
    }
    public override void OnUpdate()
    {

        if (!machine.IsMoving)
        {
            machine.ChangeState(StateMachineV3.STATE_IDLE);
        }
        else if (!machine.shiftPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_WALK);
        }
        else if (machine.jumpPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_JUMP);
        }

    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
