using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimatorEvents : MonoBehaviour
{
    public StateMachineV3 machine;

    public void AnimationEnd(int value)
    {
        switch (value)
        {
            case 0:
                machine.slashFinished = true;
                break;
            case 1:
                machine.stabFinished = true;
                break;
            case 2:
                machine.playerAnimator.SetBool("PlayerHurt", false);
                break;
            default:
                break;
        }
    }
}
