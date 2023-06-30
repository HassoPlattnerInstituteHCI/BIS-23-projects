using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5f;
    // public GameObject focalPoint;
    public bool hasItem;
    //private float powerupStrength = 30f;
    //public int powerupTime = 7;
    //public GameObject powerupIndicator;
    private SpeechIn speech;
    private SpeechOut speechOut;
    // private bool movementFrozen;
    //private UpperHandle upperHandle;

    // Start is called before the first frame update
    async void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //await ActivatePlayer();
        //speech = new SpeechIn(onSpeechRecognized);
        // speech.StartListening(new string[] { "help", "resume" });
        speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float forwardInput = Input.GetAxis("Vertical");
        //playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        //PantoMovement();
    }

    //void PantoMovement()
    //{
    //    float rotation = upperHandle.GetRotation();
    //    transform.eulerAngles = new Vector3(0, rotation, 0);
    //    playerRb.velocity = speed * transform.forward;
    //}

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


    //public async Task ActivatePlayer()
    //{
    //    upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    //    await upperHandle.SwitchTo(gameObject);
    //    upperHandle.FreeRotation();
    //}

    //void Update()
    //{
    //    if (!GameObject.FindObjectOfType<SpawnManager>().gameStarted) return;
    //    powerupIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
    //}

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Item"))
        {
            hasItem = true;
            Destroy(other.gameObject);
            speechOut.Speak("You got the item");
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    GameObject other = collision.gameObject;
        /// challenge: when collision has tag "Enemy" and we have a powerup
        /// get the enemyRigidbody and push the enemy away from the player
        //if (other.CompareTag("Wall"))
        //{
        //    Rigidbody wallRigidbody = other.GetComponent<Rigidbody>();
            //Vector3 awayFromPlayer = other.transform.position - transform.position;
            //Vector3 scaledDirection = awayFromPlayer.normalized * powerupStrength * 0.4f;
            //if (hasPowerup)
            //{
            //    scaledDirection = awayFromPlayer.normalized * powerupStrength;
            //}
            //enemyRigidbody.AddForce(scaledDirection, ForceMode.Impulse);
    //    }
    //}

    void OnApplicationQuit()
    {
        speechOut.Stop();
        speech.StopListening();
    }
}
