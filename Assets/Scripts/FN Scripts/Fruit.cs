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


    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);

        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();

        moveToFruit();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
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
        Vector3 addUP = new Vector3(0,0,0);
        Vector3 prediction = gameObject.transform.position + addUP;
        //delay umgehen?
        if (!destroyed)
            await handle.SwitchTo(gameObject, 100);
        //await handle.MoveToPosition(prediction, 10f, true);
    }

}
