using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle upperHandle;
    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.MoveToPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
