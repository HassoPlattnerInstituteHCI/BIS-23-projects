using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    // private Rigidbody playerRb;
    public float speed = 5f;
    // public GameObject focalPoint;
    public bool hasItem;
    private SpeechIn speech;
    private SpeechOut speechOut;
    // private bool movementFrozen;
    private UpperHandle upperHandle;

    // Start is called before the first frame update
    async void Start()
    {
        // playerRb = GetComponent<Rigidbody>();
        await ActivatePlayer();
        //speech = new SpeechIn(onSpeechRecognized);
        // speech.StartListening(new string[] { "help", "resume" });
        speechOut = new SpeechOut();
    }


    //async void onSpeechRecognized(string command)
    //{
    //    if (command == "resume" && movementFrozen)
    //    {
    //        ResumeAfterPause();
    //    }
    //    else if (command == "help" && !movementFrozen)
    //    {
    //        ToggleMovementFrozen();
    //        var powerups = GameObject.FindGameObjectsWithTag("Powerup");
    //        if (powerups.Length > 0)
    //        {
    //            await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(powerups[0]);
    //        }
    //    }
    //}

    //void ToggleMovementFrozen()
    //{
    //    playerRb.constraints = movementFrozen ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    //    foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        enemy.GetComponent<Rigidbody>().constraints = movementFrozen
    //                                       ? RigidbodyConstraints.None
    //                                       : RigidbodyConstraints.FreezeAll;
    //    }
    //    movementFrozen = !movementFrozen;
    //}


    public async Task ActivatePlayer()
    {
        gameObject.GetComponent<MeHandle>().enabled = false;
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.MoveToPosition(gameObject.transform.position, speed, true);
        gameObject.GetComponent<MeHandle>().enabled = true;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Item"))
        {
            hasItem = true;
            Destroy(other.gameObject);
            speechOut.Speak("You got the item");
        }
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        speech.StopListening();
    }
}
