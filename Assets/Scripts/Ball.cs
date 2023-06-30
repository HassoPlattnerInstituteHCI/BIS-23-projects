using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject Wall_l;
    [SerializeField] private GameObject Wall_r;
    [SerializeField] private GameObject Wall_u;
    [SerializeField] private GameObject Wall_d;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private float still_Speed = 0.2f;

    private bool isDragging;
    private bool inHole;
    private UpperHandle upperHandle;
    private MeHandle mehandle;

    float shoot_rotation;

    void Start()
    {
        // Find the UpperHandle component on the "Panto" GameObject and store the reference
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        // Switch the UpperHandle to control this GameObject (Ball)
        upperHandle.SwitchTo(gameObject);
        // Freeze rotation of the Rigidbody to prevent the ball from tipping over
        rb.freezeRotation = true;
    }

    async void FixedUpdate()
    {
        // Check for player input and handle ball movement
        PlayerInput();
    }

    private bool IsReady()
    {
        // Check if the ball's velocity magnitude is below the still_Speed threshold,
        // indicating that the ball is ready to be controlled
        return rb.velocity.magnitude <= still_Speed;
    }

    private bool IsTurned(float start_rotation)
    {
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = upperHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)
        {
            rotate_result = 0 - rotate_result;
        }
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= 45 && rotate_result <= 315;
    }

    private void PlayerInput()
    {
        if (!IsReady())
            return;

        // Get the position of the UpperHandle relative to the ball's position
        Vector3 inputPos = upperHandle.HandlePosition(transform.position);
        Vector2 xzinput = new Vector2(inputPos.x, inputPos.z);
        Vector2 xztransform = new Vector2(transform.position.x, transform.position.z);
        float distance = Vector2.Distance(xztransform, xzinput);

        if (distance <= 0.2f && !isDragging)
            DragStart();
        else if (IsTurned(shoot_rotation) && isDragging)
            DragRelease(xzinput);
        else if (isDragging)
            DragChange(xzinput);
    }

    private void DragStart()
    {
        // Release the UpperHandle from controlling the ball
        upperHandle.Free();
        // Store the current rotation of the UpperHandle as the shoot_rotation
        shoot_rotation = upperHandle.GetRotation();
        // Set the isDragging flag to true
        isDragging = true;
        // Set the position count of the LineRenderer to 2
        lr.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {   Wall_l.GetComponent<BoxCollider>().enabled = false;
        Wall_r.GetComponent<BoxCollider>().enabled = false;
        Wall_u.GetComponent<BoxCollider>().enabled = false;
        Wall_d.GetComponent<BoxCollider>().enabled = false;
        Vector2 xztransform = new Vector2(transform.position.x, transform.position.z);
        Vector2 dir = xztransform - pos;
        Vector2 help = xztransform + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2);
        Vector3 line = new Vector3(help.x, transform.position.y + 0.9f, help.y);

        // Update the LineRenderer positions to visualize the shooting direction
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, line);
    }

    async void DragRelease(Vector2 pos)
    {   
        Vector2 xztransform = new Vector2(transform.position.x, transform.position.z);
        float distance = Vector2.Distance(xztransform, pos);
        isDragging = false;
        lr.positionCount = 0;

        if (distance < 1f)
            return;

        Vector2 dir = xztransform - pos;
        Vector2 velocity_2 = Vector2.ClampMagnitude(dir * power, maxPower);
        Vector3 velocity = new Vector3(velocity_2.x, 0.0f, velocity_2.y);

        // Switch the UpperHandle to control the ball and set its velocity based on the shooting direction
        await upperHandle.SwitchTo(gameObject);
        Wall_l.GetComponent<BoxCollider>().enabled = true;
        Wall_r.GetComponent<BoxCollider>().enabled = true;
        Wall_u.GetComponent<BoxCollider>().enabled = true;
        Wall_d.GetComponent<BoxCollider>().enabled = true;
        rb.velocity = velocity;

        // Wait until the ball's velocity magnitude falls below the still_Speed threshold
        while (rb.velocity.magnitude > still_Speed)
        {
            await upperHandle.SwitchTo(gameObject);
        }
        // Release the UpperHandle from controlling the ball
        upperHandle.Free();
    }
}
