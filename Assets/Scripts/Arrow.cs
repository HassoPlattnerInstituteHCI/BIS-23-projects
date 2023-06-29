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
    {    upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        upperHandle.SwitchTo(gameObject);
        //upperHandle.Free();
        // This method is called before the first frame update.
        // You can add any necessary initialization code here.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // This method is called every frame.
        // It handles player input for dragging the ball.
        PlayerInput();
    }

    private bool IsReady()
    {
        // Checks if the ball is ready to be dragged.
        // It returns true if the ball's velocity magnitude is less than or equal to 0.2f.
        return rb.velocity.magnitude <= still_Speed;
    }

    private bool IsTurned(float start_rotation){
        return start_rotation != upperHandle.GetRotation();
    }

    private void PlayerInput()
    {
        // Handles the player's input for dragging the ball.

        if (!IsReady() || flying)
            return; // Exit the method if the ball is not ready to be dragged.
        
        //Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inputPos = upperHandle.HandlePosition(transform.position);
        //Vector3 inputPos = MeHandle.transform.position;

        Vector2 xzinput = new Vector2(0.0f, 0.0f);
        xzinput.x = inputPos.x;
        xzinput.y = inputPos.z;

        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;
        
        float distance = Vector2.Distance(xztransform, xzinput);

        if (distance <= 0.3f && !isDragging)
            DragStart(); // Start dragging the ball if the left mouse button is pressed and the distance is within the threshold.

        else if (IsTurned(shoot_rotation) && isDragging)
            DragRelease(xzinput); // Release the ball if the left mouse button is released and dragging is in progress.

        else if (isDragging)
            DragChange(xzinput); // Update the dragging position if the left mouse button is held and dragging is in progress.

        
    }

    private void DragStart()
    {
        // Starts the dragging process.
        upperHandle.Free();
        shoot_rotation = upperHandle.GetRotation();
        isDragging = true;
        lr.positionCount = 2; // Set the line renderer's position count to 2 (start and end points).
    }

    private void DragChange(Vector2 pos)
    {
        // Updates the dragging position.
        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;

        Vector2 dir = xztransform - pos;

        Vector2 help = xztransform + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2);

        Vector3 line = new Vector3(0.0f, 0.0f,0.0f);
        line.x = help.x;
        line.y = transform.position.y + 0.9f;
        line.z = help.y;

        lr.SetPosition(0, transform.position); // Set the starting position of the line renderer to the ball's position.
        lr.SetPosition(1, line);
        // Set the ending position of the line renderer based on the direction and power of the drag.
    }

    async void DragRelease(Vector2 pos)
    {   flying = true;
        // Releases the ball from the dragging process.
        Vector2 xztransform = new Vector2(0.0f, 0.0f);
        xztransform.x = transform.position.x;
        xztransform.y = transform.position.z;

        

        float distance = Vector2.Distance(xztransform, pos);
        isDragging = false;
        lr.positionCount = 0; // Clear the line renderer.

        if (distance < 1f)
            return; // If the distance is less than 1f, do not apply any force to the ball.

        Vector2 dir = xztransform - pos;
        Vector2 rotation = (dir.normalized) * 90;

        Vector2 velocity_2 = Vector2.ClampMagnitude(dir * power, maxPower);
        Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
        velocity.z = velocity_2.y;
        velocity.x = velocity_2.x;
        
        upperHandle.SwitchTo(gameObject);
        await Task.Delay((int)dir.magnitude * 200 +100);
        upperHandle.Free();
        transform.eulerAngles = new Vector3(90.0f, rotation.x, 0.0f);
        rb.velocity = velocity;
        await Task.Delay((int)velocity.magnitude * 700 + 400);
        transform.position = new Vector3(0.0f, 0.8f,-10.76f);
        transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        upperHandle.SwitchTo(gameObject);
        await Task.Delay(300);
        upperHandle.Free();
        flying = false;

        
        // Apply a force to the ball's rigidbody in the direction and magnitude determined by the drag.
    }
}
