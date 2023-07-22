using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrap : MonoBehaviour
{
    private GameObject[] walls;
    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls){
            wall.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        Destroy(gameObject);

        foreach(GameObject wall in walls){
            wall.active = true;
        }
    }
}
