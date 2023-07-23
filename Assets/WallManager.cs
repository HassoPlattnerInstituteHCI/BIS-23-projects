using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class WallManager : MonoBehaviour
{
    PantoCollider[] pantoColliders;
    void Start()
    {
        activateColliders();
    }

    void activateColliders()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            
            collider.CreateObstacle();
            collider.Enable();
            
        }
    }

    void Update()
    {

    }

}