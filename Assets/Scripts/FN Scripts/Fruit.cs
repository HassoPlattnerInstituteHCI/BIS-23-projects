using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class Fruit : MonoBehaviour
{

    public AudioClip destructionSound;
    public AudioClip spawnSound;

    PantoHandle handle;
    GameObject spawnManager;
    private int hitPerFruitCount = 0;
    private bool destroyed = false;

    GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().playSound("Throw1");
        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();

        moveToFruit();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.GetChild(0).transform.position = transform.position + (GetComponent<Rigidbody>().velocity / 3);

        //moveToFruit();

        Debug.Log("prediction position: " + transform.GetChild(0).transform.position); 
	    if (transform.position.z < -11.5)
        {
            destroyed = true;
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Fail();
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Katana")
        {
            FindObjectOfType<SpawnManager>().slicedFruitsCount++;
            Debug.LogWarning("Hit by Katana");
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);


            audioManager.GetComponent<AudioManager>().playSound("Slash1");

            if (FindObjectOfType<SpawnManager>().slicedFruitsCount >= FindObjectOfType<SpawnManager>().fruitsToWin)
                FindObjectOfType<SpawnManager>().Win();
            else
                FindObjectOfType<SpawnManager>().CalculateNewSpawnPosition();

            destroyed = true;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            destroyed = true;
            FindObjectOfType<SpawnManager>().Fail();
            Destroy(gameObject);
        }
    }
    async void moveToFruit()
    {
        //GameObject schon destroyed?
        //Vector3 addUP = new Vector3(0,0,0);
        //Vector3 prediction = gameObject.transform.position + addUP;
        //delay umgehen?
        if (destroyed)
            return;

        
        
	    await handle.SwitchTo(gameObject.transform.GetChild(0).gameObject, 30);

        //Falls tatsächlich während der Kurve das Objekt erreicht wird, soll wieder zum go geswitched werden
        //moveToFruit(); - Wohl doch nicht notwendig
	    
	
	    //await handle.MoveToPosition(prediction, 10f, true);
    }

}
