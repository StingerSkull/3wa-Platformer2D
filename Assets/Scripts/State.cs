using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected CubeStateV3 machine;

    public State(CubeStateV3 _machine)
    {
        machine = _machine;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();
    public abstract void OnTriggerEnter();

}
