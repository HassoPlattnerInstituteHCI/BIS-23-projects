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
    private LowerHandle lh;
    private UpperHandle meHandle;
    private float  switch_speed = 100;
    public GameObject enemy;
    private bool x = false;


    // Start is called before the first frame update
    void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
        meHandle.SwitchTo(Spawn, 100);
        Wait(2000);
        lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
        lh.SwitchTo(enemy, switch_speed);        
        sp = new SpeechOut();
        sp.Speak("Hit the Zombie. It can't move yet!");
        player.tag = "armed";
 
        
    }

    // Update is called once per frame
    void Update()
    {
       if(!GameObject.Find("Enemy")){
        SceneManager.LoadScene("ShooterLvl3");
       }
    }

async void Wait(int time){
        await Task.Delay(time);
        meHandle.Free();
    }
}
