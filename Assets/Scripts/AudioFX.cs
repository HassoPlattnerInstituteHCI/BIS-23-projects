using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;

public class AudioFX : MonoBehaviour
{
    public AudioClip rail;
    public AudioClip gameOverClip;
    public AudioClip collisionClip;
    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private GameObject previousEnemy;
    private AudioSource audioSource;
    public SpeechOut Voice;


    async void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Voice = new SpeechOut();
        await Task.Delay(1000);
        //railPlay();

        Voice.Speak("Welcome to Level 1, use the me handle to pull back and turn to shoot VAMOSsss!",1.0f, SpeechBase.LANGUAGE.ENGLISH);

        
    }
    public float PlayerFellDown()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverClip);
        return gameOverClip.length;
    }
    public void railPlay()
    {
      audioSource.PlayOneShot(rail);
    }

    public void Level1(){




    }

    public void Level2(){

    }

    public void Level3(){


    }

     public void Level4(){

        
    }
    public void Level5(){

        
    }
          
       
        
     
    public void PlayClipPitched(AudioClip clip, float minPitch, float maxPitch)
    {
        // little trick to make clip sound less redundant
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        // plays same clip only once, this way no overlapping
        audioSource.PlayOneShot(clip);
        audioSource.pitch = 1f;
    }
    
    void OnApplicationQuit() {
        Voice.Stop();
    }

}
