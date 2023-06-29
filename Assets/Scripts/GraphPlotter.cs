using UnityEngine;

public class GraphPlotter : MonoBehaviour
{
    public float xMin = -5f; // Minimum value for x-axis
    public float xMax = 5f; // Maximum value for x-axis
    public float resolution = 0.1f; // Granularity of the graph
    public float graphScale = 1f; // Scale of the graph
    public Material graphMaterial; // Material for the graph

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        GenerateGraph();
        AlignCollider();
    }
    float MathFunction(float x) {
        return x * x;
    }
    void GenerateGraph()
    {
        int numPoints = Mathf.CeilToInt((xMax - xMin) / resolution);
        Vector3[] vertices = new Vector3[2*numPoints];
        int[] tris = new int[6*(numPoints-1)];

        for (int i = 0; i < numPoints; i++)
        {
            float x = xMin + i * resolution;
            float y = MathFunction(x);

            vertices[2*i] = new Vector3(x * graphScale, y * graphScale, 0f);
            vertices[2*i + 1] = new Vector3(x * graphScale, y * graphScale, 1f);
            if(i == numPoints-1) { break; }
            tris[6*i] = 2 * i;
            tris[6*i+1] = 2 * (i+1);
            tris[6*i+2] = 2 * (i+1) + 1;
            tris[6*i+3] = 2 * (i + 1) + 1;
            tris[6*i+4] = 2 * i + 1;
            tris[6*i+5] = 2 * i;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = tris;

        meshFilter.mesh = mesh;
        GetComponent<MeshRenderer>().material = graphMaterial;
    }

    void AlignCollider()
    {
        meshCollider.sharedMesh = meshFilter.mesh;
    }
}
