using UnityEngine;
using UnityEngine.InputSystem;

public class FishController : MonoBehaviour
{
    /*  This script handles basic movement for the playable fish */

    [Header("Movement")]
    public float swimSpeed = 4f;        // speed of fish

    [Header("Turning")]
    public float turnSpeed = 100f;      // how quickly fish turns L/R
    public float pitchSpeed = 70f;      // how quickly fish moves U/D

    [Header("Control scheme")]
    public PlayerInput playerInput;    // for accessing player input

    private InputAction swimAction;    // swim button
    private InputAction turnAction;    // for turning with joystick
    private Vector2 turnInput;          

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();      // assign playerinputs component on awake

        swimAction = playerInput.actions["Swim"];
        turnAction = playerInput.actions["Turn"];
    }

    void OnEnable()     // enable input system actions 
    {
        swimAction.Enable();
        turnAction.Enable();
    }

    void OnDisable()        // disable input system actions
    {
        swimAction.Disable();
        turnAction.Disable();
    }

    void Update()
    {
        turnInput = turnAction.ReadValue<Vector2>();        // read input from joystick, adjust pitch and yaw
        float yaw = turnInput.x * turnSpeed * Time.deltaTime;
        float pitch = -turnInput.y * pitchSpeed * Time.deltaTime;

        transform.Rotate(0f, yaw, 0f, Space.World);         // only rotate horizontally around world axis
        transform.Rotate(pitch, 0f, 0f, Space.Self);

        if (swimAction.IsPressed())     // move forward by holding space/ south ctrlr button
        {
            transform.position += transform.forward * swimSpeed * Time.deltaTime;       
        }
    }
}
