using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    private Rigidbody bulletRb;
    public float bullet_speed = 8f;
    public float shooting_delay = 2f;
    private SpeechOut sp;
    private float canShoot;

    
    // Start is called before the first frame update
    void Start()
    {
        sp = new SpeechOut();
        bulletRb = bullet.GetComponent<Rigidbody>();
        canShoot = Time.time + shooting_delay;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
/*
    Vector3 xyz = new Vector3(0, 90, 0);
    Quaternion newRotation = Quaternion.Euler(xyz);
    GameObject.Instantiate(instance, this.transform.position, newRotation);

*/

    void FixedUpdate() {
        if(this.tag == "armed" && Time.time > canShoot) {
            canShoot = Time.time + shooting_delay;
            var pos = this.transform.position;
            bulletRb = Instantiate(bullet, pos, Quaternion.identity).GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * bullet_speed);

        }
    }
}