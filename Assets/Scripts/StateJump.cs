using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJump : State
{
    public StateJump(StateMachineV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.rb2d.velocity = new Vector2(machine.rb2d.velocity.x, machine.jumpForce);
    }
    public override void OnUpdate()
    {
        if (machine.rb2d.velocity.y < 0)
        {
            machine.ChangeState(StateMachineV3.STATE_FALL);
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
