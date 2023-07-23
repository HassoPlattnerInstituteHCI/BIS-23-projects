using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{   
    public GameObject cube;
    public GameObject Ball;
    
    // Start is called before the first frame update
    void Start()
    { 
        //cube = GameObject.Find("Cube");
        cube.SetActive(true);

       // Ball = GameObject.Find("Ball");
        Ball.SetActive(true);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
