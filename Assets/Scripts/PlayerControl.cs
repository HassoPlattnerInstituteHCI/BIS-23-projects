using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.IO;
using System;
using static UnityEngine.GraphicsBuffer;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle meHandle;
    PantoHandle itHandle;

    Vector3 spawnPoint;
    Vector3 goalPoint;

    public float forceValue = 1;
    public GameObject itGodObject;
    public GameObject meGodObject;
    bool shotBall = false;

    void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        //set layers here, because cant stage files from dualpantoframework
        meGodObject.layer = LayerMask.NameToLayer("GodObject");
        itGodObject.layer = LayerMask.NameToLayer("GodObject");


        spawnPoint = new Vector3(0, 0, -10);

        //für lvl 1 irrelevant
        goalPoint = Vector3.zero;

        transform.position = spawnPoint;
        meHandle.SetPositions(spawnPoint, meHandle.GetRotation(), spawnPoint);
        

    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 currentForce = spawnPoint - transform.position;

        if(!shotBall)
        {
            meHandle.ApplyForce(currentForce.normalized, currentForce.magnitude * (float)Math.Pow(forceValue, 2.0));
            if ((Math.Abs(meHandle.GetRotation()) >= 90 && Math.Abs(meHandle.GetRotation()) <= 180) || (Math.Abs(meHandle.GetRotation()) > 180 && Math.Abs(meHandle.GetRotation()) <= 270))
            {
                print("shoot");
                print("MeHandle Rotation: " + Math.Abs(meHandle.GetRotation()));
                shotBall = true;
                GetComponent<MeHandle>().enabled = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().AddForce(currentForce*(float)Math.Pow(forceValue, 2.0), ForceMode.Impulse);
            }

        }
        else
        {
            meHandle.SetPositions(transform.position, meHandle.GetRotation(), spawnPoint);

            if (((Math.Abs(itHandle.GetRotation()) >= 90 && Math.Abs(itHandle.GetRotation()) <= 180) || (Math.Abs(itHandle.GetRotation()) > 180 && Math.Abs(itHandle.GetRotation()) <= 270)) && shotBall)
            {
                print("ItHandle Rotation: " + (itHandle.GetRotation()));

                ResetLevel();

            }

        }


    }


    private void ResetLevel()
    {
        print("reset");

        shotBall = false;

        //Reset ball
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        GetComponent<MeHandle>().enabled = true;
        GetComponent<Rigidbody>().useGravity = false;

        //transform.position = spawnPoint;

        //it handle weiterspinnen verhindern?
        itHandle.SetPositions(goalPoint, 0f, goalPoint);
        meHandle.SetPositions(spawnPoint, 0f, spawnPoint);

        //...
    }

}
