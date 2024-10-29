using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFall : State
{
    public StateFall(StateMachineV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.rb2d.gravityScale = machine.fallMultiplier;
        machine.playerAnimator.SetBool("AnimStateFall", true);
    }

    public override void OnUpdate()
    {
        if (machine.isGrounded)
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
    }

    public override void OnExit()
    {
        machine.rb2d.gravityScale = 1f;
        machine.playerAnimator.SetBool("AnimStateFall", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
