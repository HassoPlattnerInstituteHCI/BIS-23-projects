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
    private SpeechIn speech;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = gravity;
        rb = GetComponent<Rigidbody>();
        speech = new SpeechIn(onSpeechRecognized);
        speech.StartListening(new string[] { "halt", "stop", "pause", "wait" });
        Invoke("letsGo", 3);
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

   void onSpeechRecognized(string command)
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
        speech.StopListening();
    }
}
