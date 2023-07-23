using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Traverse : MonoBehaviour
{   [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    [SerializeField] private GameObject Hole;
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject SpawnGuy;
    [SerializeField] private GameObject Walls3;
    [SerializeField] private GameObject Walls4;
    [SerializeField] private GameObject Walls5;


    private UpperHandle meHandle;
    private LowerHandle itHandle;
    private GameMaster gameMaster;
    
    float shot_rotation;
    
    PantoCollider pc1;
    PantoCollider pc2;
    PantoCollider pc3;
    PantoCollider pc4;

    private Vector3 spawn = new Vector3(3.0f,0.0f,-10.0f);

    bool done = false;
    bool ready = false;

    int level_rn; 

    void Start(){
      meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
      itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>(); 
      gameMaster = gameObject.GetComponent<GameMaster>(); 
      pc1 = Wall1.GetComponent<PantoCollider>();
      pc2 = Wall2.GetComponent<PantoCollider>();
      pc3 = Wall3.GetComponent<PantoCollider>();
      pc4 = Wall4.GetComponent<PantoCollider>(); 

    }

    void FixedUpdate(){
        if(IsTurned(shot_rotation)&&!done&&ready){
            TraverseFinish(level_rn);
        }

    }

    private bool IsTurned(float start_rotation){
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = meHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)rotate_result = 0 - rotate_result;
        int rotation_degree = 90;
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= rotation_degree && rotate_result <= (360 - rotation_degree);
    }
    
    public async void TraverseSetup(int level){
        done = false;
        level_rn = level;
        //AudioFx.PlayIntro(level);
        //Ball.SetActive(false);
        
        //await meHandle.MoveToPosition(spawn,50f);
        
        // pc1.CreateObstacle();
        // pc2.CreateObstacle();
        // pc3.CreateObstacle();
        // pc4.CreateObstacle();
        // pc1.Enable();
        // pc2.Enable();
        // pc3.Enable();
        // pc4.Enable();
        switch(level){
        case 3: 
            pc1.CreateObstacle();
            pc2.CreateObstacle();
            pc3.CreateObstacle();
            pc4.CreateObstacle();
            pc1.Enable();
            pc2.Enable();
            pc3.Enable();
            pc4.Enable();
            break;
        case 4: 
            foreach(Transform child in Walls4.transform){
                if(child == Walls4.transform.GetChild(0))continue;
                PantoCollider pc = child.gameObject.GetComponent<PantoCollider>();
                pc.CreateObstacle();
                pc.Enable();
            }
            pc1.CreateObstacle();
            pc2.CreateObstacle();
            pc3.CreateObstacle();
            pc4.CreateObstacle();
            pc1.Enable();
            pc2.Enable();
            pc3.Enable();
            pc4.Enable();
            break;
        case 5: 
            foreach(Transform child in Walls5.transform){
                if(child == Walls5.transform.GetChild(0))continue;
                PantoCollider pc = child.gameObject.GetComponent<PantoCollider>();
                pc.CreateObstacle();
                pc.Enable();
            }
            pc1.CreateObstacle();
            pc2.CreateObstacle();
            pc3.CreateObstacle();
            pc4.CreateObstacle();
            pc1.Enable();
            pc2.Enable();
            pc3.Enable();
            pc4.Enable();
            break;
        default: 
            break;
        }
        await Task.Delay(2000);
        meHandle.Free();
        shot_rotation = meHandle.GetRotation();
        
        ready = true;
    }

    async void TraverseFinish(int level){
        done = true;
        ready = false;
        pc1.Remove();
        pc2.Remove();
        pc3.Remove();
        pc4.Remove();
        switch(level){
        case 3: 
            // foreach(Transform child in Walls3.transform){
            //     PantoCollider pc = child.gameObject.GetComponent<PantoCollider>();
            //     pc.Remove();
            // }
            break;
        case 4: 
            foreach(Transform child in Walls4.transform){
                if(child == Walls4.transform.GetChild(0))continue;
                PantoCollider pc = child.gameObject.GetComponent<PantoCollider>();
                pc.Remove();
                //meHandle.SwitchTo(SpawnGuy,30f);
            }
            break;
        case 5: 
            foreach(Transform child in Walls5.transform){
                if(child == Walls5.transform.GetChild(0))continue;
                PantoCollider pc = child.gameObject.GetComponent<PantoCollider>();
                pc.Remove();
            }
            break;
        default: 
            break;
        }
        await Task.Delay(1000);
        gameMaster.TraverseComplete();
    }

    
}
