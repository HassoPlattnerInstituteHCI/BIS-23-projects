using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class Fruit : MonoBehaviour
{

    public AudioClip destructionSound;
    GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z > 1)
        {
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Fail();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Katana"))
        { 
            Debug.LogWarning(other.tag);
            Debug.LogWarning("Hit by Katana");
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            Destroy(gameObject);
            FindObjectOfType<SpawnManager>().Win();
        }
    }

}
