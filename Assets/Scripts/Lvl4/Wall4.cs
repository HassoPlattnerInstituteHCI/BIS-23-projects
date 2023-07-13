using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using System.Threading.Tasks;

public class Wall4 : MonoBehaviour
{
    public GameObject Obstacle;
    public GameObject WallLeft;
    public GameObject WallRight;
    public GameObject WallTop;
    public GameObject WallBottom;


    PantoCollider pc; 
    PantoCollider pc1; 
    PantoCollider pc2; 
    PantoCollider pc3; 
    PantoCollider pc4; 
    // Start is called before the first frame update
    void Start()
    {
        pc = Obstacle.GetComponent<PantoCollider>();
       TurnOn(pc);
       pc1 = WallLeft.GetComponent<PantoCollider>();
       TurnOn(pc1);
       pc2 = WallRight.GetComponent<PantoCollider>();
       TurnOn(pc2);
       pc3 = WallTop.GetComponent<PantoCollider>();
       TurnOn(pc3);
       pc4 = WallBottom.GetComponent<PantoCollider>();
       TurnOn(pc4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void TurnOn(PantoCollider pc){
        pc.CreateObstacle();
        pc.Enable();
    }

   
}
