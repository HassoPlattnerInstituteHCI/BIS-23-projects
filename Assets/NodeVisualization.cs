using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Navigator))]

public class NodeVisualization : MonoBehaviour
{
    [SerializeField]
    private bool showNodeConnections = true;

    private Navigator navigator;

    void Start()
    {
         //subscribe to list loaded event
        navigator = GetComponent<Navigator>();
        navigator.listLoadedEvent.AddListener(DrawNodes);
    }

    private void DrawNodes()
    {
        Debug.Log("Drawing nodes");
        if (showNodeConnections)
        {
            for (int i = 0; i < navigator.adjList.Length; i++)
            {
                foreach (int node in navigator.adjList[i])
                {
                    Debug.DrawLine(navigator.nodesPos[i], navigator.nodesPos[node], Color.cyan, 100f);
                }
            }
        }
    }
}
