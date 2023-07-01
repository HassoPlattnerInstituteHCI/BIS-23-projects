using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] private bool isDrawing = false;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            lineRenderer.SetPosition(1, this.transform.position);
        }
    }

    // Sets isDrawing to true. We can add additional logic to set origin of lineRenderer here.
    public void StartDrawing()
    {
        isDrawing = true;
    }

    public void StopDrawing()
    {
        isDrawing = false;
    }

    public bool IsDrawing()
    {
        return isDrawing;
    }
}
