using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;
public class TestBall : MonoBehaviour
{   
 
    float speed = 1f;
    public PantoHandle meHandle;
    public GameObject cube;
    public GameObject ball;
    public Rigidbody rb;
    public GameObject gameObject;
    public Vector3 meHandlePos;
    private Collider myCollider;
    public AudioSource pop;


    Vector3 distance = new Vector3();
    // Start is called before the first frame update
    void Start()
    {    meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        cube = GameObject.Find("Cube");
        myCollider = GetComponent<Collider>();
         rb= GetComponent<Rigidbody>();
         meHandlePos = meHandle.GetPosition();
        
        

       
      swap();
      // SpawnCube();
    
        
    }

    // Update is called once per frame
    void FixedUpdate()

    {      meHandlePos = meHandle.GetPosition();
           Vector3 distance = meHandlePos - transform.position;
        rb.AddForce(distance * speed);


        foreach(GameObject cube in GameObject.FindGameObjectsWithTag("Cube")){



            cube.transform.eulerAngles = cube.transform.eulerAngles + new Vector3(0,GameObject.FindGameObjectsWithTag("Cube").Length,0);
        }
        
       
    }

   async void swap(){

        await meHandle.SwitchTo(gameObject);
       
        meHandle.Free();
        await Task.Delay(5000);
       // SpawnCube();
    }

    void SpawnCube(){
        
       GameObject go =  Instantiate(cube, GenerateSpawnPosition(), cube.transform.rotation);
       go.AddComponent<BoxCollider>().isTrigger= true;
    }

    Vector3 GenerateSpawnPosition(){

        Vector3 RandomSpawn = new Vector3(Random.Range(0.5f,-2.5f), 0.19f ,Random.Range(-8f,-11f));
        return RandomSpawn;

    }

   private void OnTriggerEnter(Collider other){
                   

        if(other.tag == "Cube"){
            
            SpawnCube();
         //   cube.transform.eulerAngles = cube.transform.eulerAngles + new Vector3(0,45,0);
            Destroy(other);
        }
    }
}
