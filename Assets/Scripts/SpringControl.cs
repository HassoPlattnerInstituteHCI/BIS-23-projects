using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.IO;
using System;
using static UnityEngine.GraphicsBuffer;

public class SpringControl : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle handle;
    Vector3 spawnPoint;
    public float forceValue = 1;
    bool shotBall = false;

    void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        spawnPoint = new Vector3(0, 0, -5);
        transform.position = spawnPoint;
        handle.SetPositions(spawnPoint, handle.GetRotation(), spawnPoint);
        

    }

    void FixedUpdate()
    {
        Vector3 currentForce = spawnPoint - transform.position;

        if(!shotBall)
        {
            handle.ApplyForce(currentForce.normalized, currentForce.magnitude * (float)Math.Pow(forceValue, 2.0));
            if (Math.Abs(handle.GetRotation()) >= 90)
            {
                print("shoot");
                shotBall = true;
                GetComponent<MeHandle>().enabled = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().AddForce(currentForce*(float)Math.Pow(forceValue, 2.0), ForceMode.Impulse);
                print(currentForce);
            }

        }
        else
        {
            handle.SetPositions(transform.position, handle.GetRotation(), spawnPoint);
        }


    }

    void OnDrawGizmos()
    {
        
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.red;
            Gizmos.DrawLine(spawnPoint, transform.position);
        
    }
}
