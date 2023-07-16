using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthLvl2 : MonoBehaviour
{
    public int enemy_health = 50;
    public int bullet_damage = 10;


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

        if(col.gameObject.name == "Bullet(Clone)") 
            enemy_health -= bullet_damage;

    }
}