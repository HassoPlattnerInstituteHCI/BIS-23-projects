using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;
using System.Threading.Tasks;

public class DrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    PantoLineCollider collider;
    PantoLineCollider collider2;
    GameObject obstacle;
    int i;

    [SerializeField] private bool isDrawing = false;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
        collider = GameObject.Find("Line").GetComponent<PantoLineCollider>();
        obstacle = GameObject.Find("Line");
        // collider2 = GameObject.Find("Support").GetComponent<PantoLineCollider>();
        // Register callback in Speech
        // SpeechManager speech = GameObject.Find("SpeechManager").GetComponent<SpeechManager>();
        // speech.RegisterOnStart(StartDrawing);
        // speech.RegisterOnStop(StopDrawing);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartDrawing();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StopDrawing();
        }

        if (isDrawing)
        {
          
            MoveObstacle();
            // Update collider
            // collider2.end.x = this.transform.position.x;
            // collider2.end.y = this.transform.position.z;
            // collider2.CreateObstacle();
            // collider2.Enable();
            // collider.Remove();
            // collider.end.x = this.transform.position.x;
            // collider.end.y = this.transform.position.z;
            // collider.CreateObstacle();
            // collider.Enable();
            // collider2.Remove();
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

    async Task MoveObstacle()
    {
        PantoLineCollider oldCollider = obstacle.GetComponent<PantoLineCollider>();

        //clone obstacle to make sure we don't overwrite the reference to the old collider
        GameObject newObs = Instantiate(obstacle);
        oldCollider.Remove();
        LineRenderer renderer = newObs.GetComponent<LineRenderer>();
	 renderer.SetPosition(1, this.transform.position);
        Destroy(oldCollider);

       
        //lineRenderer = obstacle.GetComponent<LineRenderer>();
        PantoLineCollider collider = newObs.GetComponent<PantoLineCollider>();
        collider.end.x = this.transform.position.x;
        collider.end.y = this.transform.position.z;


        // first enable the new collider before removing the old one to make sure the user is not accidentally getting into the obstacle
        collider.CreateObstacle();
        collider.Enable();
	 Destroy(obstacle);
	 obstacle = newObs;
        //    Destroy(oldCollider);
        await Task.Delay(2000);
    }
}
