using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Intro2 : MonoBehaviour
{
    private SpeechOut sp;
    public GameObject player;
    public GameObject panto;

    // Start is called before the first frame update
    void Start()
    {
        sp = new SpeechOut();
        sp.Speak("Hit the Zombie");
        player.tag = "armed";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
