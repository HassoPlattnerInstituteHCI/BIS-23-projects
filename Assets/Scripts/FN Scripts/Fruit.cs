using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{

    public AudioClip destructionSound;
    public AudioClip spawnSound;

    PantoHandle handle;
    GameObject spawnManager;
    private int slicePerFruit = 0;
    private int slices = 0;
    private bool destroyed = false;
    public FruitType type;
    private string slashSound = "";
    private string throwSound = "";
    GameObject audioManager;

    private bool canBeHit = true;
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        audioManager = GameObject.Find("AudioManager");

        setStats();
        audioManager.GetComponent<AudioManager>().playSound(throwSound);

        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        moveToFruit();
    }

    void setStats () {
        switch (type) {
            case FruitType.Erdbeere:
                slicePerFruit = 1;
                slashSound = "Slash1";
                throwSound = "Throw1";
                break;
            case FruitType.Kokosnuss:
                slicePerFruit = 3;
                slashSound = "Slash1";
                throwSound = "Throw1";
                break;
            case FruitType.Bombe:
                slashSound = "Slash1";
                throwSound = "Throw1";
                break;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.GetChild(0).transform.position = transform.position + (GetComponent<Rigidbody>().velocity / 3);

        //moveToFruit();

	    /*if (transform.position.z < -11.5)
        {
            destroyed = true;
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Fail(type);
        }*/


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Katana")
        {
            //Vom Katane gehittet
            if (!canBeHit)
                return;

            canBeHit = false;
            slices++;
            audioManager.GetComponent<AudioManager>().playSound(slashSound);
            Debug.LogWarning("Hit by Katana");

            //Bombe -> fail
            if (this.type == FruitType.Bombe)
            {
                Debug.Log("Bombe getroffen");
                destroyed = true;
                FindObjectOfType<SpawnManager>().Fail(type);
                Destroy(gameObject);
                return;
            }

            //Wenn so oft gehittet, dass Fruit zerstört wird
            if (slices >= slicePerFruit) {
                FindObjectOfType<SpawnManager>().slicedFruitsCount++;

                if (FindObjectOfType<SpawnManager>().slicedFruitsCount >= FindObjectOfType<SpawnManager>().fruitsToWin)
                    FindObjectOfType<SpawnManager>().Win();
                else
                    FindObjectOfType<SpawnManager>().CalculateNewSpawnPosition();

                destroyed = true;
                Destroy(gameObject);
            }


        }
        else if (other.gameObject.tag == "Wall")
        {
            //Wenn Szene 6, und Bombe vorgestellt wird, dann wird gewonnen, wenn Bombe NICHT gehittet wird
            Debug.Log("wall hit");
            if (SceneManager.GetActiveScene().name == "FruitNinja 6") {
                Debug.Log("Szene 6 Bombenwin");
                FindObjectOfType<SpawnManager>().Win();
                destroyed = true;
                Destroy(gameObject);
                return;
            }

            destroyed = true;
            FindObjectOfType<SpawnManager>().Fail(type);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Katana")
        {
            canBeHit = true;
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

public enum FruitType
{
    Erdbeere,
    Kokosnuss,
    //Bombe immer am Ende, falls man die nicht spawnen will, und trotzdem random Früchte haben will. -> randomFruit die maxExlusive -1 rechnen
    Bombe
}
