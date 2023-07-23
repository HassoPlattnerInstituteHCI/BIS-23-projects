using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;
public class TestScript : MonoBehaviour


{

    private UpperHandle upperHandle;
    // Start is called before the first frame update
    void Start()
    { upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();

        upperHandle.SwitchTo(gameObject);
        upperHandle.Free();
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player"){
            upperHandle.Freeze();
        }

    }
}
