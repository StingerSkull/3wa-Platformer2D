using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeState : MonoBehaviour
{
    public SpriteRenderer cubeSprite;

    private bool spacePressed = false;
    private bool isTriggered = false;
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
        switch (currentColor)
        {
            case State.WHITE:
                if(cubeSprite.color != Color.white)
                {
                    cubeSprite.color = Color.white;
                }
                
                if(chrono >= 5f)
                {
                    currentColor = State.YELLOW;
                    chrono = 0f;
                }

                chrono += Time.deltaTime;
                break;
            case State.YELLOW:
                if (cubeSprite.color != Color.yellow)
                {
                    cubeSprite.color = Color.yellow;
                }

                if (spacePressed)
                {
                    currentColor = State.WHITE;
                }
                else if (isTriggered)
                {
                    currentColor = State.BLUE;
                }
                break;
            case State.BLUE:
                if (cubeSprite.color != Color.blue)
                {
                    cubeSprite.color = Color.blue;
                }

                if (spacePressed)
                {
                    currentColor = State.WHITE;
                }
                break;
            default:
                break;
        }
        isTriggered = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
    }
}
