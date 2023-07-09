using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Intro2 : MonoBehaviour
{
    private SpeechOut sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = new SpeechOut();
        sp.Speak("Hit the Zombie");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
