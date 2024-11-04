using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTrigger : MonoBehaviour
{
    private Vector2 mousePosition;
    private GameObject _objectToMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_objectToMove != null)
        {
            _objectToMove.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    public void PointerMove(InputAction.CallbackContext context)
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
                mousePosition = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
    }

    public void PointerClick(InputAction.CallbackContext context)
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
                RaycastHit2D rch2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(mousePosition), Mathf.Infinity);

                if (rch2D.collider != null)
                {
                    if (rch2D.collider.CompareTag("Trigger"))
                    {
                        _objectToMove = rch2D.transform.gameObject;
                    }
                }
                break;
            case InputActionPhase.Canceled:
                _objectToMove = null;
                break;
            default:
                break;
        }
    }
}
