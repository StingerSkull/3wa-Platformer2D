using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : State
{
    public StateAttack(StateMachineV3 _machine) : base(_machine)
    {

    }

    public override void OnEnter()
    {
        machine.currentSpeed = 0f;
        machine.playerAnimator.SetBool("AnimStateAttack", true);
    }

    public override void OnUpdate()
    {
        if (machine.slashPressed)
        {
            machine.playerAnimator.SetBool("SlashAttack", true);
        }
        else if (machine.stabPressed)
        {
            machine.playerAnimator.SetBool("StabAttack", true);
        }
        else if (machine.slashFinished || machine.stabFinished)
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
        machine.playerAnimator.SetBool("AnimStateAttack", false);
        machine.playerAnimator.SetBool("SlashAttack", false);
        machine.playerAnimator.SetBool("StabAttack", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter()
    {
    }

}
