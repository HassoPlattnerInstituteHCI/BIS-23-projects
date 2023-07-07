using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using SpeechIO;

public class L2SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update
        SpeechOut speechOut = new SpeechOut();
        SpeechIn speechIn;
        public string[] startDraw = new string[] { "start", "start drawing", "draw", "draw line", "line", "point", "new line" };
        public string[] stopDraw = new string[] { "stop", "end line", "end point", "end", "finish" };
        public string[] erase = new string[] { "erase", "undo", "remove", "delete", "delete line", "erase line", "remove line" };
        public string[] move = new string[] { "move", "shift", "transition", "edit" };
        public string[] select = new string[] { "select", "this" };
        public string[] commands;

        private Dictionary<string, List<Action>> methodTable = new Dictionary<string, List<Action>> ();
        private bool isWaiting = false;

    void Start()
    {
        commands = startDraw.Concat(stopDraw.Concat(erase.Concat(move.Concat(select)))).ToArray();
        speechIn = new SpeechIn(onRecognized, commands);
        Initial();
    }

    async void Initial()
    {
        await speechOut.Speak("Draw a from start to end");
    }

    // Update is called once per frame
    void Update()
    {
        recognize();
    }

    public void RegisterOnStart(Action callback)
    {
        if (!methodTable.ContainsKey("start"))
        {
            methodTable.Add("start", new List<Action>());
        }
        methodTable["start"].Add(callback);
    }

    public void UnregisterOnStart(Action callback)
    {
        if (!methodTable.ContainsKey("start"))
        {
            return;
        }
        methodTable["start"].Remove(callback);
    }

    public void RegisterOnStop(Action callback)
    {
        if (!methodTable.ContainsKey("stop"))
        {
            methodTable.Add("stop", new List<Action>());
        }
        methodTable["stop"].Add(callback);
    }

    public void UnregisterOnStop(Action callback)
    {
        if (!methodTable.ContainsKey("stop"))
        {
            return;
        }
        methodTable["stop"].Remove(callback);
    }

    public void RegisterOnErase(Action callback)
    {
        if (!methodTable.ContainsKey("erase"))
        {
            methodTable.Add("erase", new List<Action>());
        }
        methodTable["erase"].Add(callback);
    }

    public void UnregisterOnErase(Action callback)
    {
        if (!methodTable.ContainsKey("erase"))
        {
            return;
        }
        methodTable["erase"].Remove(callback);
    }

    public void RegisterOnMove(Action callback)
    {
        if (!methodTable.ContainsKey("move"))
        {
            methodTable.Add("move", new List<Action>());
        }
        methodTable["move"].Add(callback);
    }

    public void UnregisterOnMove(Action callback)
    {
        if (!methodTable.ContainsKey("move"))
        {
            return;
        }
        methodTable["move"].Remove(callback);
    }

    public void RegisterOnSelect(Action callback)
    {
        if (!methodTable.ContainsKey("select"))
        {
            methodTable.Add("select", new List<Action>());
        }
        methodTable["select"].Add(callback);
    }

    public void UnregisterOnSelect(Action callback)
    {
        if (!methodTable.ContainsKey("select"))
        {
            return;
        }
        methodTable["select"].Remove(callback);
    }


    void onRecognized(string message)
    {
        Debug.Log("[Speech]: " + message);
        if (startDraw.Contains(message) && methodTable.ContainsKey("start"))
        {
            StartCoroutine(Callback(methodTable["start"]));
        }
        else if (stopDraw.Contains(message) && methodTable.ContainsKey("stop"))
        {
            StartCoroutine(Callback(methodTable["stop"]));
        }
        else if (erase.Contains(message) && methodTable.ContainsKey("erase"))
        {
            StartCoroutine(Callback(methodTable["erase"]));
        }
        else if (move.Contains(message) && methodTable.ContainsKey("move"))
        {
            StartCoroutine(Callback(methodTable["move"]));
        }
        else if (select.Contains(message) && methodTable.ContainsKey("select"))
        {
            StartCoroutine(Callback(methodTable["select"]));
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

    public void OnApplicationQuit()
    {
        speechIn.StopListening(); // [macOS] do not delete this line!
    }

    IEnumerator Callback(List<Action> actions)
    {
        foreach(var callable in actions)
        {
            callable();
            yield return null;
        }
    }
}

