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
        speechOut.Speak("Next Try - Explore Mode");
    }
    
    public void SpeShootMode()
    {
        speechOut.Speak("Shoot Mode");
    }
    
    public void SpeWatchMode()
    {
        speechOut.Speak("Watch Mode");
    }

    public async Task SpeLvl1()
    {
        await speechOut.Speak("To shoot the golf ball draw the lower handle like a bow and wait. Release after the sound ends.");
    }
    
    public async Task SpeLvl2()
    {
        await speechOut.Speak("The upper handle points at the ball. Find it, wait and shoot it again.");
    }
    
    public async Task SpeLvl3()
    {
        await speechOut.Speak("Here we go again, but now you must feel the hole and walls with the lower handle.");
    }
}
