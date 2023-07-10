using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;

public class AudioFX : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource rail;
    private GameObject previousEnemy;
    private AudioSource audioSource;
    public SpeechOut Voice;


    async void Start()
    {
        Voice = new SpeechOut();  
    }
  
    public void Level1(){

         Voice = new SpeechOut();
         Voice.Speak("Aim by pulling back the me-handle, turn to shoot!",1.0f, SpeechBase.LANGUAGE.ENGLISH);
    }

    public void Level2(){
      Voice = new SpeechOut();
      Voice.Speak("Now that you have gotten a feel for striking, aim and try to hit the flag!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }

    public void Level3(){
      Voice = new SpeechOut();
      Voice.Speak("Traverse the course to find objects in your way, turn the handle when you are done!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
    }

     public void Level4(){
      Voice = new SpeechOut();
      Voice.Speak("Find objects and aim when ready.", 1.0F, SpeechBase.LANGUAGE.ENGLISH);
        
    }
    public void Level5(){
      Voice = new SpeechOut();
      Voice.Speak("Play the parkour!", 1.0F, SpeechBase.LANGUAGE.ENGLISH);    
    }
          
       
    
    void OnApplicationQuit() {
        Voice.Stop();
    }

}