using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJump : State
{
    public StateJump(StateMachineV3 _machine) : base(_machine)
    {
    }

    private float chrono;

    public override void OnEnter()
    {
        machine.jumpPressed = false;
        if(machine.currentSpeed == 0)
        {
            machine.currentSpeed = machine.walkSpeed;
        }
        machine.rb2d.velocity = new Vector2(machine.rb2d.velocity.x, machine.jumpForce);
        machine.playerAnimator.SetBool("AnimStateJump", true);
        chrono = 0f;
    }
    public override void OnUpdate()
    {
        if (machine.isGrounded && chrono >= 0.1f)
        {
            if (machine.IsMoving)
            {
                machine.ChangeState(StateMachineV3.STATE_WALK);
            }
            else
            {
                machine.ChangeState(StateMachineV3.STATE_IDLE);
            }
        }
        else
        {
            if (machine.rb2d.velocity.y < 0)
            {
                machine.ChangeState(StateMachineV3.STATE_FALL);
            }
            else if (machine.rb2d.velocity.y < machine.maxHeightVelocity)
            {
                machine.playerAnimator.SetBool("MaxHeightReached", true );
            }
            chrono += Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        machine.playerAnimator.SetBool("AnimStateJump", false);
        machine.playerAnimator.SetBool("MaxHeightReached", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
