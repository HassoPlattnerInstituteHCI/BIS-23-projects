using System;
using DualPantoFramework;
using SpeechIO;
using Unity.VisualScripting;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    // Components of Panto
    private LowerHandle _lowerHandle;
    private readonly String _meHandleGodObject = "ItHandleGodObject";

    public int vibrationIntensity = 15;
    private int _vibrationMultiplicationFactor = -1;

    private void Start()
    {
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If Me Handle is on Object and we're in Explore Mode: Say Goal to Identify Goal 
        if (other.gameObject.name == _meHandleGodObject && MiniGolfV2.currentGameMode == MiniGolfV2.GameModes.ExploreMode)
        { 
            SoundsMinigolf.speechOut.Speak("Goal");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If Me Handle is on Object and we're in Explore Mode: Vibrate It Handle for more haptics user 
        if (other.gameObject.name == _meHandleGodObject && MiniGolfV2.currentGameMode == MiniGolfV2.GameModes.ExploreMode)
        {
            _lowerHandle.Rotate(_lowerHandle.GetRotation() + vibrationIntensity * _vibrationMultiplicationFactor);
            _vibrationMultiplicationFactor *= -1;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // If Me Handle leave Object and we're in Explore Mode: Stop vibrating the It Handle
        if (other.gameObject.name == _meHandleGodObject && MiniGolfV2.currentGameMode == MiniGolfV2.GameModes.ExploreMode)
        {
            _lowerHandle.FreeRotation();
        }
    }
}
