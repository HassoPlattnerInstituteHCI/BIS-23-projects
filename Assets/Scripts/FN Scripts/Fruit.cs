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

        moveToFruit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Katana")
        { 
            Debug.LogWarning(other.tag);
            Debug.LogWarning("Hit by Katana");
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            Destroy(gameObject);
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
        Vector3 addUP = new Vector3(0,0,- 2);
        Vector3 prediction = gameObject.transform.position + addUP;
        await handle.MoveToPosition(prediction, 10f, true);
    }

}
