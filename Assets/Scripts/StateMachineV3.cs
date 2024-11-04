using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachineV3 : MonoBehaviour
{
    public Animator playerAnimator;
    public Rigidbody2D rb2d;
    public LayerMask groundMask;
    public Transform groundChecker;
    public Vector2 groundCheckerDimension;
    public GameManager gameManager;
    public GameObject spawn;
    public HealthBar healthBar;

    public int maxPlayerLife = 5;
    public int playerLife;
    private float chronoInvFrame;
    public float invFramesTime = 0.3f;
    public bool canBeHurt = true;

    [Header("Speeds")]
    public float currentSpeed = 0f;
    public float walkSpeed = 5f;
    public float runSpeed = 15f;
    public float maxAcceleration = 52f;
    public float maxDecceleration = 52f;
    public float maxTurnSpeed = 80f;
    public float maxAirAcceleration=52f;
    public float maxAirDeceleration=52f;
    public float maxAirTurnSpeed = 80f;

    private float maxSpeedChange;
    private float acceleration;
    private float deceleration;
    private float turnSpeed;
    //private float setSpeed;
    //private float chronoAirSpeed;
    public Vector2 velocity;

    private bool facingRight = true;

    [Header("Air Control")]
    public float jumpForce = 10f;
    public float fallMultiplier = 1.2f;
    public float maxHeightVelocity = 0.1f;

    public bool shiftPressed;
    public bool jumpPressed;
    public bool slashPressed;
    public bool stabPressed;
    public bool isGrounded;
    public bool slashFinished;
    public bool stabFinished;

    public Dictionary<string, State> _states = new();
    public State currentState;

    public const string STATE_IDLE = nameof(StateIdle);
    public const string STATE_WALK = nameof(StateWalk);
    public const string STATE_RUN = nameof(StateRun);
    public const string STATE_JUMP = nameof(StateJump);
    public const string STATE_FALL = nameof(StateFall);
    public const string STATE_ATTACK = nameof(StateAttack);

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

    */
    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawCube(groundChecker.position, groundCheckerDimension);
    }

    // Start is called before the first frame update
    void Start()
    {
        _states.Add(STATE_IDLE, new StateIdle(this));
        _states.Add(STATE_WALK, new StateWalk(this));
        _states.Add(STATE_RUN, new StateRun(this));
        _states.Add(STATE_JUMP, new StateJump(this));
        _states.Add(STATE_FALL, new StateFall(this));
        _states.Add(STATE_ATTACK, new StateAttack(this));
        ChangeState(STATE_IDLE);

        playerLife = maxPlayerLife;

    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeHurt)
        {
            chronoInvFrame += Time.deltaTime;
            if (chronoInvFrame > invFramesTime) 
            {
                canBeHurt = true;   
            }
        }
        currentState.OnUpdate();

    }
    void FixedUpdate()
    {
        /*
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
        */

        Collider2D overlapCollider = Physics2D.OverlapBox(groundChecker.position, groundCheckerDimension, 0, groundMask);
        isGrounded = overlapCollider != null;
        HorizontalMovement();

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
/*
#if UNITY_EDITOR
        Debug.Log("Exiting: " + currentState.ToString());
        Debug.Log("Entering: " + stateName);

#endif
*/
    }

    public void HorizontalMovement()
    {
        acceleration = isGrounded ? maxAcceleration : maxAirAcceleration;
        deceleration = isGrounded ? maxDecceleration : maxAirDeceleration;
        turnSpeed = isGrounded ? maxTurnSpeed : maxAirTurnSpeed;
        //chronoAirSpeed = isGrounded ? 0f : chronoAirSpeed += Time.deltaTime;
        //setSpeed = currentSpeed/(1 + chronoAirSpeed);
         

        if (IsMoving)
        {
            //If the sign (i.e. positive or negative) of our input direction doesn't match our movement, it means we're turning around and so should use the turn speed stat.
            if (Mathf.Sign(_moveDirection) != Mathf.Sign(velocity.x))
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                //If they match, it means we're simply running along and so should use the acceleration stat
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            //And if we're not pressing a direction at all, use the deceleration stat
            maxSpeedChange = deceleration * Time.deltaTime;
        }

        //Move our velocity towards the desired velocity, at the rate of the number calculated above
        Vector2 desiredVelocity = new Vector2(_moveDirection, 0f) * currentSpeed;
        velocity.x = Mathf.MoveTowards(rb2d.velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = rb2d.velocity.y;
        //Update the Rigidbody with this new velocity
        rb2d.velocity = velocity;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
                if (_moveDirection > 0 && !facingRight)
                {
                    Flip();
                }
                else if (_moveDirection < 0 && facingRight)
                {
                    Flip();
                }
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

    public void Slash(InputAction.CallbackContext context)
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
                slashPressed = true;
                break;
            case InputActionPhase.Canceled:
                slashPressed = false;
                break;
            default:
                break;
        }
    }

    public void Stab(InputAction.CallbackContext context)
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
                stabPressed = true;
                break;
            case InputActionPhase.Canceled:
                stabPressed = false;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if(canBeHurt)
            {
                canBeHurt = false;
                chronoInvFrame = 0f;
                updatePlayerLife(-1);
                playerAnimator.SetBool("PlayerHurt", true);
            }

            if (playerLife <= 0)
            {
                transform.position = spawn.transform.position;
                playerLife = maxPlayerLife;
                healthBar.ResetHearts();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            gameManager.Win();
        }
    }

    public void updatePlayerLife(int value)
    {
        playerLife += value;
        healthBar.RemoveHeart();
    }

}
