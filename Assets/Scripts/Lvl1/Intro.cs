using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class ShooterIntro : MonoBehaviour
{
    private SpeechOut sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = new SpeechOut();
        sp.Speak("Pick up the gun");
        //sp.Speak("The closer you are the louder the sound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
