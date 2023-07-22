using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class auto : MonoBehaviour
{
    // Start is called before the first frame update

    private int count = 0;
    private int max = 450;
    Vector3 test;



    void Start()
    {
        test = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    private void FixedUpdate()
    {

        if (-(test.y - transform.rotation.eulerAngles.y )<= 90f)
        {  
            gameObject.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), 10 * Time.deltaTime);
            gameObject.transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
        }


        

        

        Debug.Log(count.ToString());


    }
}
