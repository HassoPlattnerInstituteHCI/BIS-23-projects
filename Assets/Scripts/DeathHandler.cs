using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update
    SpeechOut speechOut = new SpeechOut();
    GameObject playerHandle;
    private bool first;



    void Start()
    {
        first = true;
        playerHandle = this.gameObject;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItHandle"))
        {
            if (first)
            {
                first = false;
            }
            else
            {
                await speechOut.Speak("You died!");
            }
        }
    }

}
