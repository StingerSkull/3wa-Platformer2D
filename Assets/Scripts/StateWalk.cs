using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk : State
{
    public StateWalk(CubeStateV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.blue;
        machine.bodySprite.color = Color.blue;
    }
    public override void OnUpdate()
    {
        machine.rb2d.velocity = machine.direction * machine.moveSpeed;
        if (machine.shiftPressed)
        {
            machine.ChangeState(CubeStateV3.STATE_RUN);
        }

        if (machine.direction == Vector2.zero)
        {
            machine.ChangeState(CubeStateV3.STATE_IDLE);
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
