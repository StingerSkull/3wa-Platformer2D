using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    
    public enum State
    {
        IDLE,WALK,RUN,JUMP,FALL
    };

    public State currentState = State.IDLE;

    public enum Element
    {
        FIRE = 0,ICE = 1,THUNDER = 3
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.IDLE:
                Debug.Log("IDLE");
                if(Time.time > 5f)
                {
                    Debug.Log("5s");
                    currentState = State.WALK;
                }
                /*else if (je suis touché){
                    currentState = State.FALL;
                }*/
                break;
            case State.WALK:
                Debug.Log("WALK");
                break;
            case State.RUN:
                break;
            case State.JUMP:
                break;
            case State.FALL:
                break;
            default:
                break;
        }
    }
}
