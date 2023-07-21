using UnityEngine;
using System;
using System.Collections.Generic;

public class Rumble : MonoBehaviour
{
    public float radius = 2f;
    public int numberRumblePoints = 5;
    public GameObject trackingBall;
    private Rigidbody rb;
    List<Vector3> rP;
    List<GameObject> rGo;
    // Start is called before the first frame update
    void Start()
    {
        rGo = new List<GameObject>();
        rP = calcualteRumblePoints();
        renderRumblePoints(rP);
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        destroyRumblePointsGlobal();
        rP = calcualteRumblePoints();
        renderRumblePoints(rP);
    }

    public List<Vector3> calcualteRumblePoints()
    {
        //Debug.Log("Radius: " + radius);
        Vector3 centerPosition = transform.position;
        //Debug.Log("Center: " + centerPosition);
        //Debug.Log("new vec: " + (centerPosition + new Vector3(1, 0, 0)));
        List<Vector3> rumblePoints = new List<Vector3>();


        double angleStep = Math.PI * 2 / numberRumblePoints;

        // Generates n points that span a n-sided equilateral polygon  inside a circle
        for (int i = 0; i < numberRumblePoints; i++)
        {

            Vector3 thetaVec = Quaternion.Euler(0, Convert.ToSingle(angleStep * i / (Math.PI * 2f)) * 360f, 0) * new Vector3(1, 0, 0);
            thetaVec *= radius;
            rumblePoints.Add(centerPosition + thetaVec);
        }

        return rumblePoints;
    }

    public void renderRumblePoints(List<Vector3> rumblePoints)
    {
        for (int i = 0; i < numberRumblePoints; i++)
        {
            // change color based on angle
            GameObject go = Instantiate(trackingBall, rumblePoints[i], trackingBall.transform.rotation);
            Color randColor = UnityEngine.Random.ColorHSV((1f / numberRumblePoints) * i, (1f / numberRumblePoints) * i + (1f / numberRumblePoints), 1f, 1f, 1f, 1f);
            go.GetComponent<Renderer>().material.color = randColor;
            rGo.Add(go);
        }
    }

    public void destroyRumblePointsGlobal()
    {
        foreach (GameObject go in rGo)
        {
            Destroy(go);
        }
        rGo.Clear();
    }

}
