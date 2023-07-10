using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class WallManager : MonoBehaviour
{   private UpperHandle meHandle;
    private LowerHandle itHandle;
    float shot_rotation;
    [SerializeField] private GameObject ColliderHelper;
    [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    PantoCollider pc1;
    PantoCollider pc2;
    PantoCollider pc3;
    PantoCollider pc4;
    bool on = true;
    // Start is called before the first frame update
    void Start()
    {
      meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
      itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>(); 
      pc1 = Wall1.GetComponent<PantoCollider>();
      pc2 = Wall2.GetComponent<PantoCollider>();
      pc3 = Wall3.GetComponent<PantoCollider>();
      pc4 = Wall4.GetComponent<PantoCollider>(); 
      pc1.CreateObstacle();
      pc2.CreateObstacle();
      pc3.CreateObstacle();
      pc4.CreateObstacle();
      pc1.Enable();
      pc2.Enable();
      pc3.Enable();
      pc4.Enable();
      shot_rotation = itHandle.GetRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTurned(shot_rotation)){
            shot_rotation = itHandle.GetRotation();
            if(on){
            pc1.Remove();
            pc2.Remove();
            pc3.Remove();
            pc4.Remove();
            on = false;}
            else{
            pc1.CreateObstacle();
            pc2.CreateObstacle();
            pc3.CreateObstacle();
            pc4.CreateObstacle();
            pc1.Enable();
            pc2.Enable();
            pc3.Enable();
            pc4.Enable();
            on = true;}
        }

    }

    private bool IsTurned(float start_rotation)
    {
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = itHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)rotate_result = 0 - rotate_result;
        int rotation_degree = 45;
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= rotation_degree && rotate_result <= (360 - rotation_degree);
    }
}
