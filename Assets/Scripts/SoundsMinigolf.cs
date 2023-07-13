using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeechIO;
using UnityEngine;

public class SoundsMinigolf : MonoBehaviour
{

    public AudioClip clipSuccess;
    public AudioClip clipLoad;
    
    public static SpeechOut speechOut;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();
    }

    public float SeHitGoal()
    {
        _audioSource.PlayOneShot(clipSuccess);
        return clipSuccess.length;
    }

    public float SePlayLoad()
    {
        _audioSource.clip = clipLoad;
        _audioSource.Play();
        return clipLoad.length;
    }
    
    public void SeStop()
    {
        _audioSource.Stop();
    }
    
    public void SpeExploreMode()
    {
        speechOut.Speak("Next Try - Explore Mode"); // Modus evtl auskommentieren, wenn sowieso verständlich
    }
    
    public void SpeShootMode()
    {
        speechOut.Speak("Shoot Mode");
    }
    
    public void SpeWatchMode()
    {
        speechOut.Speak("Release now to shoot. Explore mode");
    }

    public async Task SpeLvl1()
    {
        await speechOut.Speak("Draw the lower handle like a bow to shoot the ball. Wait for the sound to end.");
    }
    
    public async Task SpeLvl2()
    { 
        await speechOut.Speak("The upper handle follows the ball. Find it and hold still to enter shoot mode.");
    }
    
    public async Task SpeLvl3()
    {
        await speechOut.Speak("With the lower handle try to explore your sourroundings.");
    }
}
