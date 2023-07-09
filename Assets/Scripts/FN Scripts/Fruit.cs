using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class Fruit : MonoBehaviour
{

    public AudioClip destructionSound;
    public int force;
    PantoHandle handle;
    GameObject spawnManager;
    private int hitPerFruitCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Fail();
        }

        if (this.gameObject == FindObjectOfType<SpawnManager>().fruits[0]) 
            moveToFruit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Katana" && (this.gameObject == FindObjectOfType<SpawnManager>().fruits[0]))
        {
            FindObjectOfType<SpawnManager>().slicedFruitsCount++;
            Debug.LogWarning(other.tag);
            Debug.LogWarning("Hit by Katana");
            //Hört man das?
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            FindObjectOfType<SpawnManager>().fruits.Remove(this.gameObject);
            Destroy(gameObject);

            if (FindObjectOfType<SpawnManager>().slicedFruitsCount >= FindObjectOfType<SpawnManager>().fruitsToWin)
                FindObjectOfType<SpawnManager>().Win();
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Fail();
        }
    }
    async void moveToFruit()
    {
        //GameObject schon destroyed?
        Vector3 addUP = new Vector3(0,0,- 2);
        Vector3 prediction = gameObject.transform.position + addUP;
        //delay umgehen?
        await handle.MoveToPosition(prediction, 10f, true);
    }

}
