using UnityEngine;
using SpeechIO;
using DualPantoFramework;

public class Introduction : MonoBehaviour
{
    private SpeechIn speechIn;
    private SpeechOut speechOut;

    async void Start()
    {
        speechOut = new SpeechOut();
        speechIn = new SpeechIn(OnSpeechRecognized);
        Level level = GameObject.Find("Panto").GetComponent<Level>();
        await level.PlayIntroduction();
        await speechOut.Speak("Try to guess the shape!");
        speechIn.StartListening(new[] { "square", "rectangle" });
    }

    async void OnSpeechRecognized(string command)
    {
        if (command is "rectangle" or "square")
        {
            await speechOut.Speak("That's correct! Congratulations!");
        }
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        speechIn.StopListening();
    }
}