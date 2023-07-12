using UnityEngine;
using SpeechIO;
using System.Threading.Tasks; //note that you need to include this if you want to use Task.Delay.
using System.Collections;

public class SoundEffects : MonoBehaviour
{
    public AudioClip MissedARing;
    public AudioClip hitARing;
    public AudioClip finishedALevel;
    public AudioClip gameOverClip;
    public AudioClip planeStarting;
    public AudioClip constantlyPlane;

    public bool planeStarted = false;
    public bool planeNoiseOn = false;

    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private AudioSource audioSource;
    public SpeechOut speechOut;
    public bool soundPlaying = false;
    public bool planeSoundPlaying = false;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();

        introduction();
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speechOut.Stop();
        }

        if(planeStarted)
        {
            if(!planeNoiseOn)
            {
                PlaneNoise();    
            }
        }
    }

    public async void PlaneNoise()
    {
        planeNoiseOn = true;
        //planeSoundPlaying = true;
        audioSource.PlayOneShot(constantlyPlane, 0.001F);
        Debug.Log("Turned on plane noise");
        //StartCoroutine(WaitForPlaneEnd(constantlyPlane.length));
        planeNoiseOn = false;
        Debug.Log("Turned off plane noise");
    }
    public async void SayHello()
    {
        await speechOut.Speak("Hello");
    }

    public async void SayText(string s)
    {
        await speechOut.Speak(s);
    }

    public async void AnnounceScore(int score)
    {
        while(soundPlaying)
        {
            await Task.Delay(100);
        }
        await speechOut.Speak("Your score is " + score.ToString());
    }

    public async void AnnounceLives(int lives)
    {
        if(lives == 0) return;

        while(soundPlaying)
        {
            await Task.Delay(100);
        }

        string output = lives == 1 ? "You have one life left." : "You have " + lives.ToString() + "lives left.";
        await speechOut.Speak(output);
    }

    public async void AnnouncePoints(int score, int pointsLost)
    {
        while(soundPlaying)
        {
            await Task.Delay(100);
        }
        await speechOut.Speak("Score:" + score.ToString() + ". Misses: " + pointsLost.ToString());
    }

    public async void introduction()
    {
        // add plane starting sounds  audioSource.PlayOneShot();
        StartCoroutine(PlayAudioWithDuration(planeStarting, 2f));
        planeStarted = true;

        while(soundPlaying)
        {
            await Task.Delay(100);
        }

        SayText("Welcome to Fabulous Flight. A ring is approaching you. Try to to reach it by twisting your steering wheel. In this tutorial, you have 3 lives.");
    }

    public void MissedRing()
    {
        soundPlaying = true;
        audioSource.PlayOneShot(MissedARing);
        StartCoroutine(WaitForSoundEnd(MissedARing.length));
        return;
    }

    public void HitRing()
    {
        soundPlaying = true;
        audioSource.PlayOneShot(hitARing);
        StartCoroutine(WaitForSoundEnd(hitARing.length));
        return;
    }

    public async void CompletedLevel(int level)
    {
        soundPlaying = true;
        audioSource.PlayOneShot(finishedALevel);
        StartCoroutine(WaitForSoundEnd(finishedALevel.length));

        while(soundPlaying)
        {
            await Task.Delay(100);
        }
        SayText("Congratulations! You finished Level " + level.ToString());
    }

    private IEnumerator WaitForSoundEnd(float seconds)
    {
        Debug.Log("Coroutine for waiting started. Waiting for " + seconds + "seconds.");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Finished Coroutine.");
        soundPlaying = false;
        Debug.Log("soundPlaying set to " + soundPlaying);
    }

    private IEnumerator WaitForPlaneEnd(float seconds)
    {
        Debug.Log("Coroutine plane for waiting started. Waiting for " + seconds + "seconds.");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Finished Coroutine.");
        planeSoundPlaying = false;
        Debug.Log("soundPlaying set to " + soundPlaying);
    }

    private System.Collections.IEnumerator PlayAudioWithDuration(AudioClip audioClip, float duration)
    {
        soundPlaying = true; 
        audioSource.clip = audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);
        soundPlaying = false;

        audioSource.Stop();
    }

    void OnApplicationQuit()
    {
        speechOut.Stop(); //Windows: do not remove this line.
    }
}