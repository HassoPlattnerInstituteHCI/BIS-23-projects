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
    private float  switch_speed = 100;
    public GameObject enemy;
    private bool x = false;

    // Start is called before the first frame update
    void Start()
    {
      //  wall = gameObject.GetComponent<Wall>();
      //   wall.manualStart();
        lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
        lh.SwitchTo(enemy, switch_speed);        
        sp = new SpeechOut();
        sp.Speak("Hit the Zombie");
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
        
    }
}
