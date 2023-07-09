using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initiatePosition : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] obstacles;
    private VertWallScript vertWallManager;
    async void Start()
    {
        PantoHandle upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
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
        vertWallManager = GameObject.Find("VertWallManager").GetComponent<VertWallScript>();
        vertWallManager.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
