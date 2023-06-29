using DualPantoFramework;
using SpeechIO;
using UnityEngine;

public class GolfBallShooting : MonoBehaviour
{
    public float strength;
    public GameObject golfBall;
    private UpperHandle _lowerHandle;
    private SpeechOut _speechOut;
    private bool _shouldStartPulling;

    private async void Start()
    {
        _lowerHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _speechOut = new SpeechOut();
        
        await _speechOut.Speak("Here's your golf ball");
        await _lowerHandle.MoveToPosition(golfBall.transform.position);
        await _speechOut.Speak("Try to shoot it forward by pulling the handle back");
        
        _shouldStartPulling = true;
    }

    private void FixedUpdate()
    {
        Vector3 handlePosition = _lowerHandle.GetPosition();
        if (_shouldStartPulling && handlePosition.z < golfBall.transform.position.z)
        {
            float distance = Vector3.Distance(handlePosition, golfBall.transform.position);
            if (distance < 0.5)
            {
                _lowerHandle.ApplyForce(Vector3.zero, 0);                
            }
            else
            {
                Vector3 direction = (golfBall.transform.position - handlePosition).normalized;
                _lowerHandle.ApplyForce(direction, strength * distance);
                Debug.Log("Move Direction: " + direction + " with force: " + (strength * distance));
            }
        }
    }

    private async void OnApplicationQuit()
    {
        await _speechOut.Speak("Thanks for playing!");
        _speechOut.Stop();
    }
}