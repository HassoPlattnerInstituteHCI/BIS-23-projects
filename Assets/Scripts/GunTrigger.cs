using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Gun") {
            this.gameObject.tag = "armed";
            Destroy(col.gameObject);
        }
    }
}
