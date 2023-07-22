using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    

    public class Graph
    {
        public LinkedList<int>[] adj;
        public int size;

        public Graph(LinkedList<int>[] _adj, int _size)
        {
            adj = _adj;
            size = _size;
        }

    }

    public static List<int> FindShortestPath(Graph G, int startVert, int targetVert)
    {
        bool[] visited = new bool[G.size];
        int[] previous = new int[G.size];
        System.Collections.Generic.Queue<int> q = new System.Collections.Generic.Queue<int>();

        visited[startVert] = true;
        previous[startVert] = -1;

        q.Enqueue(startVert);

        while (q.Count > 0)
        {
            int v = q.Dequeue();

            if (v == targetVert)
            {
                // Reconstruct the shortest path
                return ReconstructPath(previous, startVert, targetVert);
            }

            foreach (int adjV in G.adj[v])
            {
                if (!visited[adjV])
                {
                    visited[adjV] = true;
                    previous[adjV] = v;
                    q.Enqueue(adjV);
                }
            }
        }

        // No path found
        return null;
    }

    public static List<int> ReconstructPath(int[] previous, int startVert, int targetVert)
    {
        List<int> path = new List<int>();

        int current = targetVert;
        while (current != -1)
        {
            path.Add(current);
            current = previous[current];
        }

        path.Reverse();

        return path;
    }

}
