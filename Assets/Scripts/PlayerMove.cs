using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public float runSpeed = 15f;

    private Vector2 direction;
    private bool shiftPressed;

    public enum State
    {
        IDLE, WALK, RUN
    };

    public State currentSate = State.IDLE;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*
    void Update()
    {
        switch (currentSate)
        {
            case State.IDLE:
                if (headSprite.color != Color.white)
                {
                    headSprite.color = Color.white;
                }
                if (bodySprite.color != Color.white)
                {
                    bodySprite.color = Color.white;
                }

                if (direction != Vector2.zero)
                {
                    currentSate = State.WALK;
                }

                break;
            case State.WALK:
                if (headSprite.color != Color.blue)
                {
                    headSprite.color = Color.blue;
                }
                if (bodySprite.color != Color.blue)
                {
                    bodySprite.color = Color.blue;
                }

                if (direction == Vector2.zero)
                {
                    currentSate = State.IDLE;
                }
                rb2d.velocity = direction * moveSpeed;

                break;
            case State.RUN:
                if (headSprite.color != Color.red)
                {
                    headSprite.color = Color.red;
                }
                if (bodySprite.color != Color.red)
                {
                    bodySprite.color = Color.red;
                }

                if (direction == Vector2.zero)
                {
                    currentSate = State.IDLE;
                }
                rb2d.velocity = direction * moveSpeed;

                break;
            default:
                break;
        }
    }
    */
    void Update()
    {
        OnStateUpdate();
    }
    private void TransitionToState(State state)
    {
        OnStateExit();
        currentSate = state;
        OnStateEnter();
    }

    private void OnStateEnter()
    {
        switch (currentSate)
        {
            case State.IDLE:
                headSprite.color = Color.white;
                bodySprite.color = Color.white;

                break;
            case State.WALK:
                headSprite.color = Color.blue;
                bodySprite.color = Color.blue;

                break;
            case State.RUN:
                headSprite.color = Color.red;
                bodySprite.color = Color.red;

                break;
            default:
                break;
        }
    }
    private void OnStateUpdate()
    {
        switch (currentSate)
        {
            case State.IDLE:
                if (direction != Vector2.zero)
                {
                    if (shiftPressed)
                    {
                        TransitionToState(State.RUN);
                    }
                    else
                    {
                        TransitionToState(State.WALK);                        
                    }
                }
                break;
            case State.WALK:
                rb2d.velocity = direction * moveSpeed;
                if (shiftPressed)
                {
                    TransitionToState(State.RUN);
                }
                if (direction == Vector2.zero)
                {
                    TransitionToState(State.IDLE);
                }
                break;
            case State.RUN:
                rb2d.velocity = direction * runSpeed;
                if (!shiftPressed)
                {
                    TransitionToState(State.WALK);
                }
                if (direction == Vector2.zero)
                {
                    TransitionToState(State.IDLE);
                }
                break;
            default:
                break;
        }
    }
    private void OnStateExit()
    {
        switch (currentSate)
        {
            case State.IDLE:

                break;
            case State.WALK:

                break;
            case State.RUN:

                break;
            default:
                break;
        }
    }

    public void Move(InputAction.CallbackContext context)
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
                direction = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                direction = Vector2.zero;
                break;
            default:
                break;
        }
    }

    public void Run(InputAction.CallbackContext context)
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
                shiftPressed = true;
                break;
            case InputActionPhase.Canceled:
                shiftPressed = false;
                break;
            default:
                break;
        }
    }
}
