using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeStateV3 : MonoBehaviour
{
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public float runSpeed = 15f;

    public Vector2 direction;
    public bool shiftPressed;

    public Dictionary<string, State> _states = new();
    public State currentState;

    public const string STATE_IDLE = nameof(StateIdle);
    public const string STATE_WALK = nameof(StateWalk);
    public const string STATE_RUN = nameof(StateRun);

    // Start is called before the first frame update
    void Start()
    {
        _states.Add(STATE_IDLE, new StateIdle(this));
        _states.Add(STATE_WALK, new StateWalk(this));
        _states.Add(STATE_RUN, new StateRun(this));
        ChangeState(STATE_IDLE);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void ChangeState(string stateName)
    {
        /*if (currentState != null)
        {
            currentState.OnExit();
        }
        Comme le point d'interrogation ? en dessous
        */
        currentState?.OnExit();
        currentState = _states[stateName];
        currentState.OnEnter();

#if UNITY_EDITOR
        Debug.Log("Exiting: " + currentState.ToString());
        Debug.Log("Entering: " + stateName);

#endif
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
