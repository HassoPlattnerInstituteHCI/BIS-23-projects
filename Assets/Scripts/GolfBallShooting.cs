using System;
using DualPantoFramework;
using SpeechIO;
using UnityEngine;

public class GolfBallShooting : MonoBehaviour
{
    public float strength;
    private UpperHandle _meHandle;
    private SpeechOut _speechOut;
    private bool _shouldStartPulling;
    private bool _saidCongratulations;
    public float maxDistance = 0;

    private async void Start()
    {
        _meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _speechOut = new SpeechOut();
        
        _speechOut.Speak("Here's your golf ball");
        await _meHandle.MoveToPosition(gameObject.transform.position);
        await _speechOut.Speak("Try to shoot it forward by pulling the handle back");
        
        _shouldStartPulling = true;
    }

    private async void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Goal")
        {
            await _speechOut.Speak("Nice shot!");
        }
    }

    private void FixedUpdate()
    {
        if (!_shouldStartPulling) return;
        
        Vector3 handlePosition = _meHandle.GetPosition();
        if (handlePosition.z < gameObject.transform.position.z)
        {
            float distance = Vector3.Distance(handlePosition, gameObject.transform.position);
            if (distance < 0.5)
            {
                _meHandle.StopApplyingForce();
            }
            else
            {
                Vector3 direction = (gameObject.transform.position - handlePosition).normalized;
                _meHandle.ApplyForce(direction, strength * distance);
                if (distance > maxDistance) maxDistance = distance;
            }
        } else if (maxDistance > 0.5)
        {
            gameObject.transform.position = new Vector3(handlePosition.x, 1.5f, handlePosition.z);;
            if (gameObject.transform.position.z < 2.5 && !_saidCongratulations)
            {
                _saidCongratulations = true;
                _speechOut.Speak("Nice shot!");
            }
        }
    }

    private async void OnApplicationQuit()
    {
        await _speechOut.Speak("Thanks for playing!");
        _speechOut.Stop();
    }
}