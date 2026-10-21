using UnityEngine;
using UnityEngine.InputSystem;

public class FishController : MonoBehaviour
{
    /*  This script handles basic movement for the playable fish */

    [Header("Movement")]
    public float swimSpeed = 4f;        // speed of fish

    private PlayerInput playerInput;    // for accessing player input
    private InputAction swimAction;     // swim button

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        swimAction = playerInput.actions["Swim"];
    }

    void OnEnable()     // enable input system actions 
    {
        swimAction.Enable();
    }

    void OnDisable()        // disable input system actions
    {
        swimAction.Disable();
    }

    void Update()
    {
        if (swimAction.IsPressed())     // move forward by holding space/ south ctrlr button
        {
            transform.position += transform.forward * swimSpeed * Time.deltaTime;       
        }
    }
}
