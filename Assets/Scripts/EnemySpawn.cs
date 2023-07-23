using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


/*spawn points:
X = -3, Z = -6
X = 3, Z = -6
X = 3, Z = -13
X = -3, Z = -13
*/




public class EnemySpawn : MonoBehaviour
{      
    public GameObject panto;
     private LowerHandle lh;
     


        GameObject GetClosestGameObject (string tag, Vector3 position) {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in gos) {
            float currentDistance = Vector3.Distance(go.transform.position, position);
     
            if (currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }
        return closest;
    }
    

     async  void FindOtherEnemy()
    {
       Vector3 playerPosition = player.transform.position;

        /*
         * TODO2: Make the it-handle track the closest enemy
         */

        GameObject closestEnemy = GetClosestGameObject("Enemy", playerPosition);
        if (closestEnemy != null)
            await lh.SwitchTo(closestEnemy,100);
    }
    
    public GameObject player;
    public GameObject enemy;
    public float delay = 10f;
    public float enemy_count = 5;
    private float canSpawn;
    private Vector3 apos;
    private Vector3 apos2;
    private Vector3 apos3;
    private Vector3 apos4;
    public int Wave = 1;

    // Start is called before the first frame update
    public bool enemyDead(){
        if(GetClosestGameObject("Enemy",player.transform.position)==null){
            return true;
        } else {
            return false;
        }
    }
    void Start()
    {
        apos = new Vector3(-3, 0, -6);
        apos2 = new Vector3(3,0,-6);
        apos3 = new Vector3(3,0,-13);
        apos4 = new Vector3(-3,0,-13);
        canSpawn = Time.time;
        Spawn(2);
        lh = panto.GetComponent<LowerHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(Wave != 6) {
                 if(!GameObject.Find("Enemy(Clone)")) {
                 Spawn(Wave);
        }
        FindOtherEnemy();
        }


       
        // if(Time.time > canSpawn) {
        //     canSpawn = Time.time + delay;
        //     Spawn();
        // }
    }

    async void Spawn(int num) {

        for(int j = 0; j < num; j++) {
            int r = Random.Range(1, 4);
            if(r==1){
                Instantiate(enemy, apos, Quaternion.identity);
                await Task.Delay(500);
            }   else if (r==2){
                Instantiate(enemy, apos2, Quaternion.identity);
                await Task.Delay(500);
            } else if (r==3){
                Instantiate(enemy, apos3, Quaternion.identity);
                await Task.Delay(500);
            } else if (r==4){
                Instantiate(enemy, apos4, Quaternion.identity);
                await Task.Delay(500);
            }
            
        }
        Wave += 1;
    }
}

