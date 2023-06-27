using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    // Start is called before the first frame update
    void Start()
    {
        // This method is called before the first frame update.
        // You can add any necessary initialization code here.
    }

    // Update is called once per frame
    void Update()
    {
        // This method is called every frame.
        // It handles player input for dragging the ball.
        PlayerInput();
    }

    private bool IsReady()
    {
        // Checks if the ball is ready to be dragged.
        // It returns true if the ball's velocity magnitude is less than or equal to 0.2f.
        return rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput()
    {
        // Handles the player's input for dragging the ball.

        if (!IsReady())
            return; // Exit the method if the ball is not ready to be dragged.

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f)
            DragStart(); // Start dragging the ball if the left mouse button is pressed and the distance is within the threshold.

        if (Input.GetMouseButton(0) && isDragging)
            DragChange(inputPos); // Update the dragging position if the left mouse button is held and dragging is in progress.

        if (Input.GetMouseButtonUp(0) && isDragging)
            DragRelease(inputPos); // Release the ball if the left mouse button is released and dragging is in progress.
    }

    private void DragStart()
    {
        // Starts the dragging process.

        isDragging = true;
        lr.positionCount = 2; // Set the line renderer's position count to 2 (start and end points).
    }

    private void DragChange(Vector2 pos)
    {
        // Updates the dragging position.

        Vector2 dir = (Vector2)transform.position - pos;

        lr.SetPosition(0, transform.position); // Set the starting position of the line renderer to the ball's position.
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
        // Set the ending position of the line renderer based on the direction and power of the drag.
    }

    private void DragRelease(Vector2 pos)
    {
        // Releases the ball from the dragging process.

        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0; // Clear the line renderer.

        if (distance < 1f)
            return; // If the distance is less than 1f, do not apply any force to the ball.

        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
        // Apply a force to the ball's rigidbody in the direction and magnitude determined by the drag.
    }
}
