using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : State
{
    public StateRun(CubeStateV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.red;
        machine.bodySprite.color = Color.red;
    }
    public override void OnUpdate()
    {
        machine.rb2d.velocity = machine.direction * machine.runSpeed;
        if (!machine.shiftPressed)
        {
            machine.ChangeState(CubeStateV3.STATE_WALK);
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
