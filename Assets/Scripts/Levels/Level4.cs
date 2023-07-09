using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
public class Level4 : MonoBehaviour
{ 
    public SpeechOut Voice;

    
   
   async void Start()
    { 
       Voice = new SpeechOut();

       
        //await Task.Delay(1000);
        intro();
        
      
        
    }

    // Update is called once per frame
    
    public void intro(){
      
      Voice.Speak("Find objects and aim when ready.", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }
    
     void OnApplicationQuit() {
        Voice.Stop();
    }
}

