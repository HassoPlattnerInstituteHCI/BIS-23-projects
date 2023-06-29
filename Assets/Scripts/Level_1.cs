using UnityEngine;

public class Level_1 : MonoBehaviour
{
    public DualPantoFramework.Level level;
    private KeyCode startKey = KeyCode.P;
    public string introductionSpeech = "A function was plotted, what is the degree of it? - Use the Me Handle to feel it";

    private bool levelStarted = false;

    public KeyCode StartKey { get => startKey; set => startKey = value; }

    private void Update()
    {
        if (!levelStarted && Input.GetKeyDown(StartKey))
        {
            StartLevel();
        }
    }

    private async void StartLevel()
    {
        levelStarted = true;

        await level.speechOut.Speak(introductionSpeech);
        await level.PlayIntroduction();

        // Additional actions or logic after the level introduction
        // ...

        level.speechOut.Stop();
    }
}
