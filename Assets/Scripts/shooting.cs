using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    public float bullet_speed = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(this.tag == "armed") {
            var pos = this.transform.position;
            Instantiate(bullet, pos, Quaternion.identity);        
            }
    }
}
