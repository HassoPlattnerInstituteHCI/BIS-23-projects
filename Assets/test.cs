using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[] Walls;

    void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject wall in Walls)
        {
            //wall.SetActive(false);

            wall.GetComponent<Collider>().enabled = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        
        //destroy this
    
        Destroy(gameObject);

        foreach (GameObject wall in Walls)
        {
            //wall.SetActive(true);
            wall.GetComponent<Collider>().enabled = true;
        }

        gameObject.SetActive(false);
    }
    
}