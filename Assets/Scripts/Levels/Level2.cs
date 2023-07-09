using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using SpeechIO;

public class Level2 : MonoBehaviour

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
      
      Voice.Speak("Now that you have gotten a feel for striking, aim and try to hit the flag!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }
    
     void OnApplicationQuit() {
        Voice.Stop();
    }
}
