using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using DualPantoFramework;

using PlaneGame;

using UnityEngine;

public class Plane : MonoBehaviour
{
    private PantoHandle meHandle;
    private GameObject ring;
    private float rotation;
    private Plane plane;
    
    // Start is called before the first frame update
    async void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        plane = FindObjectOfType<Plane>();
        /*await meHandle.SwitchTo(plane.gameObject);
        Debug.LogWarning("Moved.");
        meHandle.Freeze();
        Debug.LogWarning("Froze.");
        meHandle.FreeRotation();
        Debug.LogWarning("Freed rotation.");*/
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            ring = GameObject.FindGameObjectsWithTag("ring")[0];
            rotation = meHandle.GetRotation();

            ring.transform.position = new Vector3(ring.transform.position.x + MapAngleToForce(rotation), 0, ring.transform.position.z);
        }
        catch (Exception)
        {
            Debug.Log("Plane standing by, no rings present.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ring")
        {
            Destroy(other.gameObject);
            ScoreManager.Score++;
        }
    }

    static float MapAngleToForce(float angle)
    {
        float force = 0;
        bool neg = false;
        
        if (angle is < 90 or > 270)
        {
            if (angle >= 270)
            {
                angle = 360 - angle;
                neg = true;
            }

            force = 1F / 90F * angle;

        }
        else
        {
            if (angle is < 270 and > 180)
            {
                angle = 360 - angle;
                neg = true;
            }

            force = 1F / 180F * angle;
        }

        return neg ? 0.04F * force : 0.04F * -force;
    }
}
