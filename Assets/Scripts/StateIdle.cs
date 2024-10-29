using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    public StateIdle(StateMachineV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.white;
        machine.bodySprite.color = Color.white;
        machine.currentSpeed = 0f;
    }
    public override void OnUpdate()
    {
        if (machine.IsMoving)
        {
            if (machine.shiftPressed)
            {
                machine.ChangeState(StateMachineV3.STATE_RUN);
            }
            else
            {
                machine.ChangeState(StateMachineV3.STATE_WALK);
            }
        }
        else if (machine.jumpPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_JUMP);
        }
    }
    public override void OnFixedUpdate()
    {
    }

    public override void OnExit()
    {
    }


    public override void OnTriggerEnter()
    {
    }

}
