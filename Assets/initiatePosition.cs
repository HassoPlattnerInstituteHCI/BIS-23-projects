using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initiatePosition : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        PantoHandle upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            GameObject obstacle = obstacles[i];
            obstacle.GetComponent<PantoBoxCollider>().Disable();
            obstacle.GetComponent<BoxCollider>().enabled = false;
        }
        
        await upperHandle.MoveToPosition(new Vector3(0f, 0f, -4f));
        for (int i = 0; i < obstacles.Length; i++)
        {
            GameObject obstacle = obstacles[i];
            obstacle.GetComponent<PantoBoxCollider>().Enable();
            obstacle.GetComponent<BoxCollider>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
