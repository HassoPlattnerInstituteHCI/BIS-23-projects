using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class BoxManager : MonoBehaviour
{
    Collider meineBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
            GetComponent<PantoBoxCollider>().isPassable = false;
        
    }
}
