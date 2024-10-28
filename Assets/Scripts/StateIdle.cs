using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    public StateIdle(CubeStateV3 _machine) : base(_machine)
    {
    }

    public override void OnEnter()
    {
        machine.headSprite.color = Color.white;
        machine.bodySprite.color = Color.white;
    }
    public override void OnUpdate()
    {
        if (machine.direction != Vector2.zero)
        {
            if (machine.shiftPressed)
            {
                machine.ChangeState(CubeStateV3.STATE_RUN);
            }
            else
            {
                machine.ChangeState(CubeStateV3.STATE_WALK);
            }
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
