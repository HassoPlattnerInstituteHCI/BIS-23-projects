using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] private bool isDrawing = false;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
        // Register callback in Speech
        L2SpeechManager speech = GameObject.Find("L2SpeechManager").GetComponent<L2SpeechManager>();
        speech.RegisterOnStart(StartDrawing);
        speech.RegisterOnStop(StopDrawing);
        speech.RegisterOnErase(UndoLine);
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
        if (!isDrawing)
        {
            startPosition = this.transform.position;
            lineRenderer.SetPosition(0, startPosition);
            isDrawing = true;
        }
    }

    public void StopDrawing()
    {
        isDrawing = false;
    }

    public bool IsDrawing()
    {
        return isDrawing;
    }

    public void UndoLine()
    {
        if (!isDrawing)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        };
    }
}