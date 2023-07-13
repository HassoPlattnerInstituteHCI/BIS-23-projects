using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*spawn points:
X = -3, Z = -6
X = 3, Z = -6
X = 3, Z = -13
X = -3, Z = -13
*/




public class EnemySpawn : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public float delay = 10f;
    public float enemy_count = 5;
    private float canSpawn;
    private Vector3[] pos;
    private Vector3 apos;

    // Start is called before the first frame update
    void Start()
    {
        apos = new Vector3(-3, 0, -6);
        canSpawn = Time.time;
        pos[0] = new Vector3(-3, 0, -6);
        pos[1] = new Vector3(3, 0, -6);
        pos[2] = new Vector3(3, 0, 13);
        pos[3] = new Vector3(-3, 0, -13);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        if(Time.time > canSpawn) {
            canSpawn = Time.time + delay;
            Spawn();
        }
    }

    void Spawn() {

        for(int i = 0; i > 4; i++ ) {
            if((player.transform.position - pos[i]).magnitude > apos.magnitude )
                apos = player.transform.position - pos[i];
        }
    
        Instantiate(enemy, apos, Quaternion.identity);

    }
}
