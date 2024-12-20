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
        machine.currentSpeed = machine.runSpeed;
        machine.playerAnimator.SetBool("AnimStateRun", true);
    }
    public override void OnUpdate()
    {
        if (machine.slashPressed || machine.stabPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_ATTACK);
        }
        else if (!machine.isGrounded && machine.rb2d.velocity.y < 0)
        {
            machine.ChangeState(StateMachineV3.STATE_FALL);
        }
        else if (!machine.IsMoving)
        {
            machine.ChangeState(StateMachineV3.STATE_IDLE);
        }
        else if (!machine.shiftPressed)
        {
            machine.ChangeState(StateMachineV3.STATE_WALK);
        }
        else if (machine.jumpPressed && machine.isGrounded)
        {
            machine.ChangeState(StateMachineV3.STATE_JUMP);
        }

    }

    public override void OnExit()
    {
        machine.playerAnimator.SetBool("AnimStateRun", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
