using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
public class Level3 : MonoBehaviour
{ 
    public SpeechOut Voice;

    
   
   async void Start()
    { 
       Voice = new SpeechOut();

       
       // await Task.Delay(1000);
        intro();
        
      
        
    }

    // Update is called once per frame
    
    public void intro(){
      
      Voice.Speak("Traverse the course to find objects in your way, turn the handle when you are done!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }
    
     void OnApplicationQuit() {
        Voice.Stop();
    }
}
