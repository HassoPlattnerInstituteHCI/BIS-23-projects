using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using SpeechIO;

public class Level1 : MonoBehaviour

{  // public GameObject GameControl;
    public SpeechOut Voice;
      //public float maxPitch = 1.2f;
  //  public float minPitch = 0.8f;
    // Start is called before the first frame update
    
    // public AudioFX soundEffects;
   async void Start()
    { 
       Voice = new SpeechOut();

        // GameControl = GameObject.FindGameObjectWithTag("GameController");
        // soundEffects = GameControl.GetComponent<AudioFX>();
        //audioSource = GetComponent<AudioSource>;
        await Task.Delay(1000);
        intro();
        
       // testvoice();
        
    }

    // Update is called once per frame
    
    public void intro(){
      
      Voice.Speak("......Aim by pulling back the Me-handle, turn to shoot!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }
    
     void OnApplicationQuit() {
        Voice.Stop();
    }
}
