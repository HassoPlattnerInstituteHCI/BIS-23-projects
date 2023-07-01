using UnityEngine;
using SpeechIO;
using DualPantoFramework;

public class IntroductionManager : MonoBehaviour
{
    SpeechOut speech;
    public SpawnManager spawnManager;

    async void Start()
    {
        speech = new SpeechOut();
        Level level = GameObject.Find("Panto").GetComponent<Level>();
        await level.PlayIntroduction();
        await speech.Speak("This is the fruit, now slice it!");
        spawnManager.startGame();
    }

    void OnApplicationQuit()
    {
        speech.Stop();
    }

}
