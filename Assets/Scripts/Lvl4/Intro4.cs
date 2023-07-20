using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Intro4 : MonoBehaviour
{
    private SpeechOut sp;
    public GameObject enemy;
    private float  switch_speed = 100;
    private PantoHandle lh;
    public GameObject panto;
    private Vector3 hector;
    public GameObject Spawn;
    private UpperHandle meHandle;
    public GameObject player;
    Wall wall;

    // Start is called before the first frame update
    void Start()
    {
        player.gameObject.tag = "armed";
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
        meHandle.SwitchTo(Spawn);
        Wait(1000);
        meHandle.Free();
        sp = new SpeechOut(); 
        sp.Speak("Evade and shoot the zombies");
        lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
        
        wall = gameObject.GetComponent<Wall>();
        wall.manualStart();
        sp = new SpeechOut(); 
        sp.Speak("Evade and shoot the zombies");
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
async void Wait(int time){
        await Task.Delay(time);
       
    }

    void swth() {
        lh.SwitchTo(enemy, switch_speed);
    }
}
