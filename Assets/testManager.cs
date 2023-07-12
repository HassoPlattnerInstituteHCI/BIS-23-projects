using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;

public class testManager : MonoBehaviour
{
    GameObject obstacle;
    PantoCollider pantoCollider;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GameObject.Find("CompoundCollider");
        pantoCollider = obstacle.GetComponent<PantoCompoundCollider>();

        pantoCollider.CreateObstacle();
        pantoCollider.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
