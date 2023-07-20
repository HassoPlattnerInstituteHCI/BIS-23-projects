using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePositionDemoLvl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] obstacles;
    PantoHandle upperHandle;
    PantoHandle lowerHandle;
    IndicatorControl indicatorControl;

    async void Start()
    {
        indicatorControl = GameObject.FindGameObjectWithTag("IndicatorBody").GetComponent<IndicatorControl>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            GameObject obstacle = obstacles[i];

            obstacle.GetComponent<BoxCollider>().enabled = false;
        }
        await lowerHandle.MoveToPosition(new Vector3(0f, 0f, -5f));
        await upperHandle.MoveToPosition(new Vector3(0f, 0f, -5f));
        for (int i = 0; i < obstacles.Length; i++)
        {
            GameObject obstacle = obstacles[i];
            obstacle.GetComponent<PantoCollider>().CreateObstacle();
            obstacle.GetComponent<PantoCollider>().Enable();
            obstacle.GetComponent<BoxCollider>().enabled = true;
        }
        indicatorControl.enableControl();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
