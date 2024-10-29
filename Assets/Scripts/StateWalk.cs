using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : State
{
    public StateWalk(StateMachineV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.blue;
        machine.bodySprite.color = Color.blue;
        machine.currentSpeed = machine.walkSpeed;
    }
    public override void OnUpdate()
    {
        if (!machine.IsMoving)
        {
            machine.ChangeState(StateMachineV3.STATE_IDLE);
        }
        else if (machine.shiftPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_RUN);
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
