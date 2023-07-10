using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;

public class Intro3 : MonoBehaviour
{
    private SpeechOut sp;
    public GameObject enemy;
    public float  switch_speed = 20;
    private PantoHandle lh;
    public GameObject panto;
    private Vector3 hector;

    // Start is called before the first frame update
    void Start()
    {
       sp = new SpeechOut(); 
       sp.Speak("Evade and shoot the zombies");
       //lh = panto.GetComponent<LowerHandle>();
       lh = GameObject.Find("Panto").GetComponent<LowerHandle>();
       //enemy = GameObject.Find("Enemy");
       lh.SwitchTo(enemy, switch_speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
