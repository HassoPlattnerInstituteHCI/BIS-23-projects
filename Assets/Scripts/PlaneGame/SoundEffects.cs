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
    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private AudioSource audioSource;
    public SpeechOut speechOut;
    public bool soundPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speechOut.Stop();
        }
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

    //TODO Freeplay sonification
    // public async void AnnounceFreePlay()
    // {

    // }

    public async void AnnounceLevel(int levelNumber)
    {
        //TODO: say level and goal of level for all existing levels
        // switch(levelNumber)
        // {
        //     case 1:

        // }
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

    void OnApplicationQuit()
    {
        speechOut.Stop(); //Windows: do not remove this line.
    }
}