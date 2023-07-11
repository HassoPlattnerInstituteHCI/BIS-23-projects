using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Traverse : MonoBehaviour
{   private UpperHandle meHandle;
    private LowerHandle itHandle;
    float shot_rotation;
    [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Ball;
    PantoCollider pc1;
    PantoCollider pc2;
    PantoCollider pc3;
    PantoCollider pc4;
    private Vector3 ballposition;
    bool done = false;
    bool ready = false;
    private GameMaster gameMaster;

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
            TraverseFinish();
        }

    }

    private bool IsTurned(float start_rotation){
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = meHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)rotate_result = 0 - rotate_result;
        int rotation_degree = 45;
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= rotation_degree && rotate_result <= (360 - rotation_degree);
    }
    
    public async void TraverseSetup(int level){
        done = false;
        await itHandle.SwitchTo(Target,30f);
        await meHandle.SwitchTo(Ball,50f);
        await Task.Delay(1000);
        Ball.SetActive(false);
        pc1.CreateObstacle();
        pc2.CreateObstacle();
        pc3.CreateObstacle();
        pc4.CreateObstacle();
        pc1.Enable();
        pc2.Enable();
        pc3.Enable();
        pc4.Enable();
        shot_rotation = meHandle.GetRotation();
        meHandle.Free();
        ready = true;
    }

    async void TraverseFinish(){
        done = true;
        pc1.Remove();
        pc2.Remove();
        pc3.Remove();
        pc4.Remove();
        Ball.SetActive(true);
        //Ball.transform.eulerAngles.y  = meHandle.GetRotation();
        await meHandle.SwitchTo(Ball);
        await Task.Delay(1000);
        gameMaster.TraverseComplete();
    }

    
}
