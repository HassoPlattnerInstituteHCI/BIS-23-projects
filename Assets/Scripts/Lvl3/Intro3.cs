using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Intro3 : MonoBehaviour
{
    private SpeechOut sp;
    public GameObject enemy;
    private float  switch_speed = 100;
    private PantoHandle lh;
    public GameObject panto;
    private Vector3 hector;
    private GameObject enemies;
    public GameObject Spawn;
    private UpperHandle meHandle;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player.gameObject.tag = "armed";
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
        meHandle.SwitchTo(Spawn,100);
        Wait(1000);
        meHandle.Free();
        sp = new SpeechOut(); 
        sp.Speak("Evade and shoot the zombies");
        lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
        lh.SwitchTo(enemy, switch_speed);
        sp = new SpeechOut(); 
        sp.Speak("Evade and shoot the zombies");
        
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
