using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.IO;

public class SpringControl : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle handle;
    Vector3 spawnPoint;
    public float forceValue = 1;

    void Start()
    {
        spawnPoint = new Vector3(0, 0, 0);
        transform.position = spawnPoint;
        handle = GameObject.Find("Panto").GetComponent<UpperHandle>();

    }

    // Update is called once per frame
    void Update()
    {
        handle.ApplyForce((spawnPoint - transform.position).normalized, (spawnPoint - transform.position).magnitude * forceValue);
    }
}
