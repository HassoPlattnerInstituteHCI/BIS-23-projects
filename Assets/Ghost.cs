using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public int startIndex;

    public int goingToIndex;

    private List<int> path;

    private bool isMoving;

    public Navigator navigator;

    private bool init = false;

    private float distanceToNodeThreshold = 0.1f;


    private int switchTimer;
    private int switchTimerMax = 200;

    

    //List<int> path = navigator.CalculateShortestPath(startIndex, 5);

    void Start()
    {
        switchTimer = switchTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (++switchTimer > switchTimerMax)
        {
            switchTimer = 0;
            path = navigator.CalculateShortestPath(startIndex, navigator.closestPacManIndex);
        }

        if (DistanceToNode(goingToIndex) < distanceToNodeThreshold)
        {
            isMoving = false;
            startIndex = goingToIndex;  
        }

        if (isMoving) 
        {
             if (DistanceToNode(goingToIndex) < distanceToNodeThreshold)
            {
                isMoving = false;
            }

            navigator.MoveGhostToNode(gameObject, navigator.nodes[goingToIndex]);
        }

        if ( (path.Count >  0) && (!isMoving) )
        {
            if (path[0] == startIndex)
            {
                path.RemoveAt(0);

                if (path.Count > 0)
                {
                    goingToIndex = path[0];
                    isMoving = true;
                }

            }   
        }

    }

    private float DistanceToNode(int nodeIndex)
    {
        return Vector3.Distance(gameObject.transform.position, navigator.nodesPos[nodeIndex]);
    }

}
