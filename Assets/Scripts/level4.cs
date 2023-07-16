using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;

public class level4 : MonoBehaviour
{
    private GameObject line;
    private LineRenderer lineRenderer;
    private PantoLineCollider lineCollider;

    private bool isEditing = false;

    private UpperHandle upperHandle;
    private LowerHandle lowerHandle;

    private int selectedPointId = -1;

    public float snapRadius = 1.0f;

    void Start() {
        line = GameObject.Find("Line");

        lineRenderer = line.GetComponent<LineRenderer>();
        lineCollider = line.GetComponent<PantoLineCollider>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();

        L4SpeechManager speech = GameObject.Find("L4SpeechManager").GetComponent<L4SpeechManager>();
        speech.RegisterOnStart(StartEditing);
        speech.RegisterOnStop(StopEditing);
    }

    void Update() {
        Vector3[] points = GetLinePoints(lineRenderer);
        if (!isEditing) {
            for (int i=0; i<2; i++) {
                int k = (int)Mathf.Abs(i-1);
                Vector3 movable = points[i];
                Vector3 fixture = points[k];

                float d = Vector3.Distance(movable, upperHandle.GetPosition());

                if (d <= snapRadius && Input.GetKeyDown(KeyCode.S))
                {
                    isEditing = true;

                    StartEditing();

                    this.transform.position = movable;
                    lowerHandle.SetPositions(fixture, 0f, fixture);
                    selectedPointId = i;

                    lineCollider.Disable();
                }
            }
        }
        else {
            lineRenderer.SetPosition(selectedPointId, this.transform.position - lineRenderer.transform.position);

            if (Input.GetKeyDown(KeyCode.E)) {

                StopEditing();

                Vector3 lhPos = lowerHandle.GetPosition();

                lineCollider.start.x = this.transform.position.x;
                lineCollider.start.y = this.transform.position.z;
                
                lineCollider.end.x = lhPos.x;
                lineCollider.end.y = lhPos.z;

                lineCollider.Enable();

                selectedPointId = -1;
                isEditing = false;
            }
        }
        
    }

    private void StartEditing() {
        return;
    }

    private void StopEditing() {
        return;
    }

    private Vector3[] GetLinePoints(LineRenderer lr) {
        Vector3[] array = { lr.GetPosition(0) + lr.transform.position, lr.GetPosition(1) + lr.transform.position};
        
        return array;
    }
}
