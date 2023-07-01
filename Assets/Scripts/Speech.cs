using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update
        SpeechOut speechOut = new SpeechOut();
        SpeechIn speechIn;
        GameObject playerHandle;
        DrawLine drawingScript;
        public string[] commands = new string[] { "line", "stop" };
        private bool isWaiting = false;

    void Start()
        {
        playerHandle = GameObject.Find("Player");
        drawingScript = playerHandle.GetComponent<DrawLine>();
        speechIn = new SpeechIn(onRecognized);
        Initial();
    }

    async void Initial()
        {
            await speechOut.Speak("Say line to start drawing, stop drawing with stop. Finish the octagon!");
        }

    // Update is called once per frame
    void Update()
    {
        recognize();
    }

    void onRecognized(string message)
    {
        Debug.Log("[Speech]: " + message);
        switch (message)
        {
            case "line":
                if (!drawingScript.IsDrawing())
                {
                    drawingScript.StartDrawing();
                }
                break;

            case "stop":
                if (drawingScript.IsDrawing())
                {
                    drawingScript.StopDrawing();
                }
                break;
        }
        
    }



    async void recognize()
    {
        // This is ofc not thread safe or anything, we gotta secure that critical section
        if (!isWaiting)
        {
            isWaiting = true;
            await speechIn.Listen(commands);
            isWaiting = false;
        }
        
    }
}

