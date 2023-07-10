using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public bool hasItem;
    private SpeechIn speech;
    private SpeechOut speechOut;
    private UpperHandle upperHandle;

    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await ActivatePlayer();
        //speech = new SpeechIn(onSpeechRecognized);
        // speech.StartListening(new string[] { "help", "resume" }); 
        speechOut = new SpeechOut();
    }

    public async Task ActivatePlayer()
    {
        await upperHandle.MoveToPosition(gameObject.transform.position, speed, true);
    }

    private void FixedUpdate() {
        transform.position = (upperHandle.HandlePosition(transform.position));
        // transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
    }

    private async void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Item"))
        {
            if(other.gameObject.GetComponent<Item>().currentItem){
                StartCoroutine(CollisionCorrutine(other.gameObject));
            }
        }
    }

    private IEnumerator CollisionCorrutine(GameObject item){
        GameObject lm = GameObject.Find("GameManager");
        lm.GetComponent<LabyrinthManager>().playItemSound(item);
        Destroy(item);
        yield return new WaitForSeconds(5f);
        lm.GetComponent<LabyrinthManager>().getNextItem();
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        // speech.StopListening();
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
}
