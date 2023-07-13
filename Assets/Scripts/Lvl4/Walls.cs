using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using System.Threading.Tasks;

public class Walls : MonoBehaviour
{   
    private SpeechOut sp;
    public GameObject enemy;
    public float  switch_speed = 30;
    private LowerHandle lh;
    public GameObject Obstacle;
    public GameObject WallLeft;
    public GameObject WallRight;
    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject Spawn;

    PantoCollider pc; 
    PantoCollider pc1; 
    PantoCollider pc2; 
    PantoCollider pc3; 
    PantoCollider pc4; 
    private UpperHandle meHandle;

    // Start is called before the first frame update
    void Start()
    {

       meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
       meHandle.SwitchTo(Spawn);
       Wait(1000);
       lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
       lh.SwitchTo(enemy, switch_speed);
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
       sp = new SpeechOut(); 
       sp.Speak("Evade and shoot the zombies");
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnOn(PantoCollider pc){
        pc.CreateObstacle();
        pc.Enable();
    }

    async void Wait(int time){
        await Task.Delay(time);
        meHandle.Free();
    }
}
