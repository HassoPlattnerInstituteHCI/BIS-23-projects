using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{   
    public float speed = 10f;
    private SpeechIn speech;
    private SpeechOut speechOut;
    private UpperHandle upperHandle;


    async void Start()
    {
        //DisableWalls();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        /*await MoveToInitPos()*/;
        await upperHandle.MoveToPosition(transform.position, 5f, true);


        //speech = new SpeechIn(onSpeechRecognized);
        // speech.StartListening(new string[] { "help", "resume" }); 
        speechOut = new SpeechOut();
        EnableWalls();
    }

    // Does not work reliably -_- Perhaps set a closer labyrinth entry?
    async Task MoveToInitPos()
    {
        await upperHandle.MoveToPosition(transform.position, 5f, true);
        Vector3 handlePos = upperHandle.GetPosition();
        float dist = Vector3.Distance(handlePos, transform.position);

        int maxIter = 10;
        while (dist > 1 && maxIter-- > 0) {
            await upperHandle.MoveToPosition(transform.position, 5f, true);
            handlePos = upperHandle.GetPosition();
            dist = Vector3.Distance(new Vector3(handlePos.x, 0, handlePos.z), new Vector3(transform.position.x, 0, transform.position.z));

        }

    }

    //void DisableWalls()
    //{
    //    PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
    //    foreach (PantoCollider collider in pantoColliders)
    //    {
    //        collider.Disable();
    //    }
    //}

    /**
     * Call this initially to render walls
     * */
    void EnableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }

    // public async Task ActivatePlayer()
    // {
    //     await upperHandle.MoveToPosition(gameObject.transform.position, speed, true);
    // }

    private void FixedUpdate() {
        Vector3 moveTo = (upperHandle.HandlePosition(transform.position));
        Debug.Log(moveTo);
        transform.position = moveTo;
        //transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
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
