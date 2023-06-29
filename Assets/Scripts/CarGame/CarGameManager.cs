using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DualPantoFramework;
using SpeechIO;
using UnityEngine;

public class CarGameManager : MonoBehaviour
{/*
    GameObject upper;
    GameObject lower;
    private SpeechOut speech;
    bool isRunning = false;
    public int currentLevel = 0;
    private int maxLevel = 1;
    Vector3 startPos = new Vector3(0, 0, -4);
    PantoHandle handle;

    async void Start()
        {
            upper = GameObject.FindGameObjectWithTag("MeHandle");
            lower = GameObject.FindGameObjectWithTag("ItHandle");
            isRunning = true;
            speech = new SpeechOut();
            Debug.Log("Starting game");
            await speech.Speak("Overtake the cars in front of you without hitting them.");
            StartLevel();
        }

    async private void StartLevel()
    {
        isRunning = false;
        //DisableForceFields();
        await Task.Delay(100);
        handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await handle.MoveToPosition(startPos, 1f, true);
        await speech.Speak("Level: " + currentLevel, 1);
        //SpawnForceFields();
        isRunning = true;
    }

    async public void LevelCompleted()
    {
        if (isRunning)
        {
            isRunning = false;
            if (currentLevel == maxLevel)
            {
                await speech.Speak("Congratulations: You've finished all levels", 1);
                return;
            }
            await speech.Speak("Level " + currentLevel + " completed", 1);
            currentLevel++;
            StartLevel();
        }
    }

     async void RestartLevel()
    {
        isRunning = false;
        await speech.Speak("You lost. Restarting Level");
        StartLevel();
    }

    void Update()
    {
        // do sth

        RestartLevel();
    }
*/
}