using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BFS;
using UnityEngine.Events;


public class Navigator : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 5f)]
    private float nodeConnectionRange;

    public GameObject[] nodes;

    public Vector3[] nodesPos;

    private GameObject[] ghosts;

    private int[] startingAtNodeIndex; // for each Ghost, which Node Index it currently moving towards              // for each Ghost, if its currently trying to move to a node

    public LinkedList<int>[] adjList;

    // list loaded event
    public UnityEvent listLoadedEvent;

    public int closestPacManIndex;

    private GameObject pacMan;

    // Start is called before the first frame update
    void Start()
    {
        pacMan = GameObject.FindGameObjectWithTag("PacMan");
        nodes = GameObject.FindGameObjectsWithTag("Node");
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        closestPacManIndex = FindClosestNodeIndex(pacMan);

        startingAtNodeIndex = new int[ghosts.Length];
        nodesPos = new Vector3[nodes.Length];
        adjList = new LinkedList<int>[nodes.Length];

        for (int i = 0; i < nodesPos.Length; i++)
        {
            nodesPos[i] = nodes[i].transform.position;
        }

        for (int i = 0; i < startingAtNodeIndex.Length; i++)
        {
            startingAtNodeIndex[i] = FindClosestNodeIndex(ghosts[i]);
            ghosts[i].GetComponent<Ghost>().startIndex = startingAtNodeIndex[i];
            ghosts[i].transform.position = nodes[startingAtNodeIndex[i]].transform.position;
        }

        for (int i = 0; i < adjList.Length; i++)
        {
            adjList[i] = findAdj(i);
        }

        listLoadedEvent.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        listLoadedEvent.Invoke();
        closestPacManIndex = FindClosestNodeIndex(pacMan);

    }
    public void MoveGhostToNode(GameObject ghost, GameObject node)
    {
        Vector3 temp = new Vector3();

        temp = (node.transform.position - ghost.transform.position);
        temp.Normalize();
        ghost.transform.position += temp / 50;
    }
    int FindClosestNodeIndex(GameObject ghost) //or PacMan I guess
    {
        int ir = -1;

        float minDist = float.MaxValue;

        for (int i = 0; i < nodes.Length; i++)
        {
            if ( (nodes[i].transform.position - ghost.transform.position).magnitude < minDist)
            {
                ir = i;
                minDist = (nodes[i].transform.position - ghost.transform.position).magnitude;
            }
        }
        return ir;
    }
    LinkedList<int> findAdj (int index)
    {
        LinkedList<int> adj = new LinkedList<int> ();

        for (int i = 0; i < nodes.Length    ; i++)
        {
            if (i != index)
            {
                if ((nodesPos[i] - nodesPos[index]).magnitude < nodeConnectionRange)
                {
                    adj.AddLast(i);
                }
            }

        }
        return adj;

    }

    public List<int> CalculateShortestPath(int startNodeIndex, int endNodeIndex)
    {
        if (startNodeIndex < 0 || startNodeIndex >= nodes.Length ||
            endNodeIndex < 0 || endNodeIndex >= nodes.Length)
        {
            Debug.LogError("Invalid node index");
            return null;
        }

        return BFS.FindShortestPath(new BFS.Graph(adjList, adjList.Length), startNodeIndex, endNodeIndex);
    }



}
