using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using System.Threading.Tasks;


public class FNIntroductionManager : MonoBehaviour
{
    private bool introEnded = false;
    SpeechOut speech;
    Level level;

    void Start()
    {
        speech = new SpeechOut();
        level = GameObject.Find("Panto").GetComponent<Level>();
    }

    async public Task PlayIntro()
    {
        await level.PlayIntroduction();
    }

    //public bool hasIntroEnded()
    //{
    //    return introEnded;
    //}

    void OnApplicationQuit()
    {
        speech.Stop();
    }

}
