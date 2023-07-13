using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Intro2 : MonoBehaviour
{
    private SpeechOut sp;
    public GameObject player;
    public GameObject panto;
     public GameObject Spawn;
       private PantoHandle lh;
     private UpperHandle meHandle;
     public float  switch_speed = 30;
     public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
        meHandle.SwitchTo(Spawn);
        Wait(1000);
        meHandle.Free();
        sp = new SpeechOut();
        sp.Speak("Hit the Zombie");
        player.tag = "armed";
        lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
        lh.SwitchTo(enemy, switch_speed);
        
    }

    // Update is called once per frame
    void Update()
    {
       if(!GameObject.Find("Enemy")){
        SceneManager.LoadScene("ShooterLvl4");
       }
    }

async void Wait(int time){
        await Task.Delay(time);
        
    }
}
