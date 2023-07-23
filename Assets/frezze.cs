using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class frezze : MonoBehaviour
{
    Rigidbody rb;
    bool frozen;
    [SerializeField]
    Vector3 gravity = new Vector3(0, -9.81f, 0);
    Vector3 lastVel;
    private SpeechIn speechIn;
    private SpeechOut speechOut = new SpeechOut();
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravity;
        rb = GetComponent<Rigidbody>();
        speechIn = new SpeechIn(OnRecognized);
        speechIn.StartListening();
        //Invoke("letsGo", 3);
        //Dialog();
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }


    async void Dialog()
    {
        await speechOut.Speak("Hello!");
        await speechIn.Listen(new string[] { "Hello", "Hi", "Hey" });
        await speechOut.Speak("How are you doing?");
        await speechIn.Listen(new string[] { "I'm fine", "Nah", "I'm Sick" });
        //...
    }

    void OnRecognized(string message)
    {
        Debug.Log("[" + this.GetType() + "]: " + message);
        return;
    }
    // Update is called once per frame
    void Update()
    {
     
    }

    void letsGo()
    {
        rb.velocity = new Vector3(0, 12, -4);
        return;
    }

    void onRecognized(string command)
    {
        Debug.Log(command);
        if (!frozen)
        {
            lastVel = rb.velocity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            frozen = !frozen;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = lastVel;
            frozen = !frozen;
        }
    }

    private void OnApplicationQuit()
    {
        //speech.StopListening();
        speechIn.StopListening();
    }
}


