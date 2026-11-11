using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    /*  This script handles basic movement for the playable fish */

    [Header("Movement")]
    public float acceleration = 20f;
    public float maxSwimSpeed = 20f;        // max speed of fish
    public float coastDrag = 2f;            // how quickly fish slows down after button is let go
    public float brakeForce = 25f;          // brake strength
    public float reverseAcceleration = 10f; 
    public float maxReverseSpeed = 15f;     
    public float stopThreshold = 0.5f;      // anything below this speed we count as 'stationary' so reversing works smoothly

    [Header("Turning")]
    public float turnSpeed = 80f;          // how quickly fish turns L/R (degrees per sec at full steer)
    public float sidewaysGrip = 8f;         // higher -> less sliding

    [Header("Ground")]
    public float groundCheckDistance = 0.6f;
    public LayerMask groundMask = ~0;       // layers that count as ground

    [Header("Control scheme")]
    public PlayerInput playerInput;    // for accessing player input

    private InputAction swimAction;    // swim button
    private InputAction turnAction;    // for turning with joystick
    private InputAction brakeAction;    // braking
    private Vector2 turnInput;
    private bool swimHeld;
    private bool brakeHeld;

    private Rigidbody rb;
    private bool grounded;
    private Vector3 groundNormal = Vector3.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();                 // assign components and controls on awake
        playerInput = GetComponent<PlayerInput>();

        swimAction = playerInput.actions["Swim"];
        turnAction = playerInput.actions["Turn"];
        brakeAction = playerInput.actions["Brake"];
    }

    void OnEnable()     // enable input system actions 
    {
        swimAction.Enable();
        turnAction.Enable();
        brakeAction.Enable();
    }

    void OnDisable()        // disable input system actions
    {
        swimAction.Disable();
        turnAction.Disable();
        brakeAction.Disable();
    }

    void Update()
    {
        turnInput = turnAction.ReadValue<Vector2>();
        swimHeld = swimAction.IsPressed();
        brakeHeld = brakeAction.IsPressed();
    }

    void FixedUpdate()
    {
        // ground check, may make more sense to take this out later since we are in water and fish can swim if they arent touching the floor
        float rayStart = 0.5f; 
        grounded = Physics.Raycast(transform.position + Vector3.up * rayStart, Vector3.down, out RaycastHit hit, rayStart + groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
        groundNormal = grounded ? hit.normal : Vector3.up;

        // direction of travel, works on slops (i think)
        Vector3 swimDir = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;
        Vector3 vel = rb.linearVelocity;
        float forwardSpeed = Vector3.Dot(vel, swimDir);
        
        // moving forward, backward, and braking
        if (brakeHeld)
        {
            if (forwardSpeed > stopThreshold)       // if still moving forward, brake
            {
                rb.AddForce(-swimDir * brakeForce, ForceMode.Acceleration);
            }
            else if (forwardSpeed > -maxReverseSpeed)       // if stopped or already reversing, accelerate backwards
            {
                rb.AddForce(-swimDir * reverseAcceleration, ForceMode.Acceleration);
            }
        }
        else if (swimHeld)      // accelerate up to top speed when button held
        {
            if (forwardSpeed < maxSwimSpeed)
            {
                rb.AddForce(swimDir * acceleration, ForceMode.Acceleration);
            }
        }
        else   // slow down (coast) when no button held
        {
            rb.AddForce(-swimDir * forwardSpeed * coastDrag, ForceMode.Acceleration);
        }

        // steering
        float steerFactor = Mathf.Clamp(forwardSpeed / 2f, -1f, 1f);
        float yaw = turnInput.x * turnSpeed * steerFactor * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yaw, 0f));

        // grippiness
        float lateralSpeed = Vector3.Dot(vel, transform.right);
        rb.AddForce(-transform.right * lateralSpeed * sidewaysGrip, ForceMode.Acceleration);
    }
}
