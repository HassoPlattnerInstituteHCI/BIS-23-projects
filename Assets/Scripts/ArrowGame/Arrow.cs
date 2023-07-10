using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Arrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject go;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private float still_Speed = 0.2f;

    private bool isDragging;
    private bool inHole;
    private UpperHandle upperHandle;

    float shoot_rotation;

    bool flying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the UpperHandle component from the "Panto" GameObject
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        upperHandle.SwitchTo(gameObject);
        go.transform.position = new Vector3(0.0f, 0.8f, -10.76f);

        // Initialize any necessary variables or components.
        // This method is called before the first frame update.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Handle player input for dragging the arrow.
        PlayerInput();
    }

    private bool IsReady()
    {
        // Check if the arrow is ready to be dragged.
        // Return true if the arrow's velocity magnitude is less than or equal to still_Speed.
        return rb.velocity.magnitude <= still_Speed;
    }

    private bool IsTurned(float start_rotation)
    {
        // Check if the arrow has been rotated.
        // Return true if the difference between the current rotation and the start_rotation is within the specified range.
        float rotate_result = upperHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)
        {
            rotate_result = 0 - rotate_result;
        }
        return rotate_result >= 45 && rotate_result <= 315;
    }

    private void PlayerInput()
    {
        // Handle the player's input for dragging the arrow.

        if (!IsReady() || flying)
            return; // Exit the method if the arrow is not ready to be dragged or is already flying.

        Vector3 inputPos = upperHandle.HandlePosition(transform.position);

        Vector2 xzinput = new Vector2(0.0f, 0.0f);
        xzinput.x = inputPos.x;
        xzinput.y = inputPos.z;

        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;

        float distance = Vector2.Distance(xztransform, xzinput);

        if (distance <= 0.3f && !isDragging)
            DragStart(); // Start dragging the arrow if the distance is within the threshold and dragging is not in progress.

        else if (IsTurned(shoot_rotation) && isDragging)
            DragRelease(xzinput); // Release the arrow if it has been rotated and dragging is in progress.

        else if (isDragging)
            DragChange(xzinput); // Update the dragging position if dragging is in progress.
    }

    private void DragStart()
    {
        // Start the dragging process.
        upperHandle.Free();
        shoot_rotation = upperHandle.GetRotation();
        isDragging = true;
        lr.positionCount = 2; // Set the line renderer's position count to 2 (start and end points).
    }

    private void DragChange(Vector2 pos)
    {
        // Update the dragging position.
        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;

        Vector2 dir = xztransform - pos;

        Vector2 help = xztransform + Vector2.ClampMagnitude((dir * power) / 7, maxPower / 7);

        Vector3 line = new Vector3(0.0f, 0.0f, 0.0f);
        line.x = help.x;
        line.y = transform.position.y + 0.9f;
        line.z = help.y;

        lr.SetPosition(0, transform.position); // Set the starting position of the line renderer to the arrow's position.
        lr.SetPosition(1, line);
        // Set the ending position of the line renderer based on the direction and power of the drag.
    }

    async void DragRelease(Vector2 pos)
    {
        // Release the arrow from the dragging process.
        flying = true;
        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;

        float distance = Vector2.Distance(xztransform, pos);
        isDragging = false;
        lr.positionCount = 0; // Clear the line renderer.

        if (distance < 1f)
        {
            flying = false;
            await upperHandle.SwitchTo(go);
            upperHandle.Free();
            return;
        }
        // If the distance is less than 1f, do not apply any force to the arrow.

        Vector2 dir = xztransform - pos;
        Vector2 normal_dir = (dir.normalized);

        float Arrow_rotation = 90.0f * normal_dir.x;

        Vector2 velocity_2 = Vector2.ClampMagnitude(dir * power, maxPower);
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
        velocity.z = velocity_2.y;
        velocity.x = velocity_2.x;

        //upperHandle.SwitchTo(gameObject);
        //await Task.Delay((int)dir.magnitude * 200 + 100);
        await upperHandle.SwitchTo(go);
        transform.eulerAngles = new Vector3(90.0f, Arrow_rotation, 0.0f);
        rb.velocity = velocity;
        await Task.Delay((int)velocity.magnitude * 70 + 450);
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0.0f, 0.8f, -10.76f);
        transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        upperHandle.Free();
        flying = false;
    }
}
