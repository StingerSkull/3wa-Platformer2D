using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachineV3 : MonoBehaviour
{
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public Rigidbody2D rb2d;

    [Header("Speeds")]
    public float currentSpeed = 0f;
    public float walkSpeed = 5f;
    public float runSpeed = 15f;

    [Header("Air Control")]
    public float jumpForce = 10f;
    public float fallMultiplier = 1.2f;

    public bool shiftPressed;
    public bool jumpPressed;

    public Dictionary<string, State> _states = new();
    public State currentState;

    public const string STATE_IDLE = nameof(StateIdle);
    public const string STATE_WALK = nameof(StateWalk);
    public const string STATE_RUN = nameof(StateRun);
    public const string STATE_JUMP = nameof(StateJump);
    public const string STATE_FALL = nameof(StateFall);

    public bool IsMoving
    {
        get { return _moveDirection != 0f; }
    }
    private float _moveDirection = 0f;

    /*
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUILayout.TextArea("TEST");
    }
#endif

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        _states.Add(STATE_IDLE, new StateIdle(this));
        _states.Add(STATE_WALK, new StateWalk(this));
        _states.Add(STATE_RUN, new StateRun(this));
        _states.Add(STATE_JUMP, new StateJump(this));
        _states.Add(STATE_FALL, new StateFall(this));
        ChangeState(STATE_IDLE);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(_moveDirection * currentSpeed, rb2d.velocity.y);
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
                _moveDirection = context.ReadValue<Vector2>().x;
                break;
            case InputActionPhase.Canceled:
                _moveDirection = 0f;
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

    public void Jump(InputAction.CallbackContext context)
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
                jumpPressed = true;
                break;
            case InputActionPhase.Canceled:
                jumpPressed = false;
                break;
            default:
                break;
        }
    }
}
