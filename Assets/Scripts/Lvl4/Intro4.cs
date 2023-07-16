using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using System.Threading.Tasks;

public class Intro4 : MonoBehaviour
{   
    private SpeechOut sp;
    public GameObject enemy;
    private float  switch_speed = 100;
    private LowerHandle lh;
    private UpperHandle meHandle;
    public GameObject Spawn;
    

    // Start is called before the first frame update
    void Start()
    {

       meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
       meHandle.SwitchTo(Spawn);
       Wait(1000);
        meHandle.Free();
       lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
       lh.SwitchTo(enemy, switch_speed);
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
       
    }
}
