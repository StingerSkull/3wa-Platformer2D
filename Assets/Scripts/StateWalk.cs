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
        machine.currentSpeed = machine.walkSpeed;
        machine.playerAnimator.SetBool("AnimStateWalk", true);
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
        else if (machine.jumpPressed && machine.isGrounded)
        {
            machine.ChangeState(StateMachineV3.STATE_JUMP);
        }
        else if (!machine.isGrounded && machine.rb2d.velocity.y < 0)
        {
            machine.ChangeState(StateMachineV3.STATE_FALL);
        }

    }

    public override void OnExit()
    {
        machine.playerAnimator.SetBool("AnimStateWalk", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
