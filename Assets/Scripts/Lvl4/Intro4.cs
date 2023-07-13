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
    public float  switch_speed = 40;
    private LowerHandle lh;
    public GameObject panto;
    private Vector3 hector;
    private GameObject enemies;
    public GameObject Obstacle;
    PantoCollider pc; 
    bool on = false;


    // Start is called before the first frame update
    void Start()
    { 
      pc = Obstacle.GetComponent<PantoCollider>();
      
       sp = new SpeechOut(); 
       sp.Speak("Evade and shoot the zombies");
       //lh = panto.GetComponent<LowerHandle>();
       lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
       //enemy = GameObject.Find("Enemy");
       lh.SwitchTo(enemy, switch_speed);

       TurnOn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  if(!GameObject.Find("Enemy")){
      //   SceneManager.LoadScene("ShooterLvl4");
      //  }
    }

    void TurnOn(){
        pc.CreateObstacle();
        pc.Enable();
    }
}
