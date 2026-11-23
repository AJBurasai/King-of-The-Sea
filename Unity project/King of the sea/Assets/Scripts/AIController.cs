using UnityEngine;

public class AIController : MonoBehaviour
{
    /* this script handles simple waypoint following opponent for prototyping */

    public Transform waypointParent;        // parent object holding the waypoints as children

    [Header("Movement")]            // movement very similar to player
    public float acceleration = 20f;
    public float maxSpeed = 21f;            // keep slightly above player speed so they can catch up as player takes shortcut
    public float turnSpeed = 80f;
    public float sidewaysGrip = 8f;
    public float coastDrag = 2f;

    [Header("Path following")]     
    public float waypointRadius = 4f;       // how close counts as "reached"
    public float cornerSlowdown = 0.5f;     // speed multiplier on sharp turns (0.5 = half speed)

    private Rigidbody rb;
    private int currentWaypoint = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypointParent == null || waypointParent.childCount == 0) return;

        Transform target = waypointParent.GetChild(currentWaypoint);

        Vector3 toTarget = target.position - transform.position;    // direction to the target, flattened so height differences don't matter
        toTarget.y = 0f;

        
        if (toTarget.magnitude < waypointRadius)    // move on to the next waypoint, looping back to the start
        {
            currentWaypoint = (currentWaypoint + 1) % waypointParent.childCount;
            return;
        }

        float angle = Vector3.SignedAngle(transform.forward, toTarget, Vector3.up);     // steering, -1 to 1 depending on how far off-angle the target is
        float steer = Mathf.Clamp(angle / 45f, -1f, 1f);

        float yaw = steer * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yaw, 0f));

        float targetSpeed = Mathf.Lerp(maxSpeed, maxSpeed * cornerSlowdown, Mathf.Abs(steer));      // speed, slow down the harder the AI has to turn
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        if (forwardSpeed < targetSpeed)
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        else
            rb.AddForce(-transform.forward * forwardSpeed * coastDrag, ForceMode.Acceleration);

        float lateralSpeed = Vector3.Dot(rb.linearVelocity, transform.right);               // sideways grip, same as player
        rb.AddForce(-transform.right * lateralSpeed * sidewaysGrip, ForceMode.Acceleration);
    }

    void OnDrawGizmos()         // draws the path in the Scene view so you can see it
    {
        if (waypointParent == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            Vector3 a = waypointParent.GetChild(i).position;
            Vector3 b = waypointParent.GetChild((i + 1) % waypointParent.childCount).position;
            Gizmos.DrawSphere(a, 0.5f);
            Gizmos.DrawLine(a, b);
        }
    }
}
