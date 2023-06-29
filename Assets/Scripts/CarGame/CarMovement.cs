using UnityEngine;
using DualPantoFramework;
using System;

public class CarMovement : MonoBehaviour
{
    PantoHandle lowerHandle;
    public float movementSpeed = 0.8f; // Speed of movement
    int count = 0; //count to alter speed
    private float targetZ = -11f; // Target Z-coordinate

    private void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        Vector3 position = ResetObject();
        lowerHandle.SetPositions(position, 0f, position);
    }

    private void Update()
    {
        // Move the object along the Z-axis
        transform.Translate((-Vector3.forward) * movementSpeed * Time.deltaTime);

        // Check if the object has reached the target
        if (transform.position.z <= targetZ)
        {
            if(count >= 5)
            {
                movementSpeed = 5f;
            }
            else if(count >= 10)
            {
                movementSpeed = 10f;
            }

            ResetObject();
            count++;
        }
        else
        {
            return;
        }
    }

    private Vector3 ResetObject()
    {
        // Place the object at a random point in the upper frame
        float randomX = new System.Random().Next(-3, 7);
        float y = 0f;
        float z = -5f;
        transform.position = new Vector3(randomX, y, z);
        return transform.position;
    }
}