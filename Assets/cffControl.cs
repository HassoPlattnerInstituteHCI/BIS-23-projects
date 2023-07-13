using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class cffControl : MonoBehaviour
{

    private GameObject[] pois;
    private GameObject panto;

    // Start is called before the first frame update
    void Start()
    {
        pois = GameObject.FindGameObjectsWithTag("poi");
        panto = GameObject.Find("Panto");
    }

    // Update is called once per frame
    void Update()
    {
        //Get pantos lower handle position
        Vector3 handlePos = panto.GetComponent<LowerHandle>().GetPosition();

        Vector3 closestPoint = Vector3.zero;
        float closestDist = -1;

        //Iterate over every gameObject we could snap onto
        foreach( GameObject go in pois )
        {
            //Get the closest point between each gameObjects collider and our handles position;
            //This is especially important for edges in the diagram!
            Vector3 closestPointOnCollider = go.GetComponent<Collider>().ClosestPoint(handlePos);

            //compute the distance between that closest point and the handle
            //We use the vectors squared magnitude since it is easier to compute and we do not need the actual vector's magnitude
            float sqrDist = (closestPointOnCollider - handlePos).sqrMagnitude;

            //If the closest gameObject is not set or we find an object that is closer to the handle:
            //Save that as the closest one!
            if ( closestDist == -1 || sqrDist < closestDist ) {
                closestPoint = closestPointOnCollider;
                closestDist = sqrDist;
            }
        }

        gameObject.transform.position = closestPoint;
    }
}
