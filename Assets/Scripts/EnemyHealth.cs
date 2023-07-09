using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemy_health;
    public int bullet_damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(enemy_health <= 0) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col) {

        if(col.gameObject.name == "Bullet") 
            enemy_health -= bullet_damage;
    }
}
