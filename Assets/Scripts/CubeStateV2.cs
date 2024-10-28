using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeStateV2 : MonoBehaviour
{
    public SpriteRenderer cubeSprite;

    private bool spacePressed = false;
    private float chrono;

    public enum State
    {
        WHITE, YELLOW, BLUE
    };

    public State currentColor = State.WHITE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }
    private void TransitionToState(State state)
    {
        OnStateExit();
        currentColor = state;
        OnStateEnter();
    }

    private void OnStateEnter()
    {
        switch (currentColor)
        {
            case State.WHITE:

                cubeSprite.color = Color.white;

                break;
            case State.YELLOW:

                cubeSprite.color = Color.yellow;


                break;
            case State.BLUE:

                cubeSprite.color = Color.blue;

                break;
            default:
                break;
        }
    }
    private void OnStateUpdate()
    {
        switch (currentColor)
        {
            case State.WHITE:
                chrono += Time.deltaTime;
                if (chrono >= 5f)
                {
                    TransitionToState(State.YELLOW);

                }
                break;
            case State.YELLOW:
                if (spacePressed)
                {
                    TransitionToState(State.WHITE);
                }

                break;
            case State.BLUE:
                if (spacePressed)
                {
                    TransitionToState(State.WHITE);
                }

                break;
            default:
                break;
        }
    }
    private void OnStateExit()
    {
        switch (currentColor)
        {
            case State.WHITE:
                chrono = 0f;

                break;
            case State.YELLOW:

                break;
            case State.BLUE:

                break;
            default:
                break;
        }
    }

    private void OnStateTriggerEnter2D(Collider2D collision)
    {
        switch (currentColor)
        {
            case State.WHITE:

                break;
            case State.YELLOW:
                TransitionToState(State.BLUE);
                break;
            case State.BLUE:

                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnStateTriggerEnter2D(collision);
    }

    public void PressSpace(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                spacePressed = true;
                break;
            case InputActionPhase.Canceled:
                spacePressed = false;
                break;
            default:
                break;
        }
    }

}
