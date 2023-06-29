using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttraction : MonoBehaviour
{
    public float attractionForce = 5f;
    private GameObject nearestLineObject;

    private void Update()
    {
        FindNearestLineObject();
        ApplyAttraction();
    }

    private void FindNearestLineObject()
    {
        GameObject[] lineObjects = GameObject.FindGameObjectsWithTag("line");
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject lineObject in lineObjects)
        {
            float distance = Vector3.Distance(transform.position, lineObject.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestLineObject = lineObject;
            }
        }
    }

    private void ApplyAttraction()
    {
        if (nearestLineObject != null)
        {
            Vector3 direction = nearestLineObject.transform.position - transform.position;
            GetComponent<Rigidbody>().AddForce(direction.normalized * attractionForce);
        }
    }
}