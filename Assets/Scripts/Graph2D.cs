using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph2D : MonoBehaviour
{
    public float xMin = -5f; // Minimum value for x-axis
    public float xMax = 5f; // Maximum value for x-axis
    public float resolution = 0.1f; // Granularity of the graph
    public float graphScale = 1f; // Scale of the graph
    public EdgeCollider2D col;

    void Start()
    {
        int numPoints = Mathf.CeilToInt((xMax - xMin) / resolution);
        Vector2[] vertices = new Vector2[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float x = xMin + i * resolution;
            float y = Mathf.Sin(x); // Replace this line with your desired mathematical function

            vertices[i] = new Vector2(x * graphScale, y * graphScale);
        }
        col.points = vertices;
    }
}
