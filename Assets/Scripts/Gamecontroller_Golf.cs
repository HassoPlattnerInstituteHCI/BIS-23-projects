using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Gamecontroller_Golf : MonoBehaviour
{   
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Hole;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Obstacle1;

    private PantoHandle itHandle;
    private PantoHandle meHandle;
    private GameObject MeHandleGodObject;
    private AudioFX soundFX;
    private Ball_movement Movement;
    private int level = 0;
    private Vector3 spawn;
    bool traversing = false;
    float shoot_rotation;
     
    // Start is called before the first frame update
    void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        MeHandleGodObject= GameObject.Find("MeHandleGodObject");
        soundFX = gameObject.GetComponent<AudioFX>();
        Movement = Ball.GetComponent<Ball_movement>();

        spawn = new Vector3(3.0f,0.0f,-10.0f);
        Ball.transform.position = spawn;
        
        Level1();
    }

    // Update is called once per frame
    void Update()
    {
     if(IsTurned(shoot_rotation)&&traversing){
        reverse();
     }   
    }

    void Level1(){
        level = 1;
        soundFX.Level1();
        Ball.SetActive(true);

    }

    void Level2(){
        soundFX.Level2();
        traverse();
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);
        
    }

    public void LevelComplete(){
        level++;
        Movement.inHole = false;
        switch (level){
            case 1:
            Level1();
            break;

            case 2:
            Level2();
            break;

            case 3:
            Level3();
            break;

            case 4:
            Level4();
            break;

            case 5:
            Level5();
            break;

            default:
            End();
            break;
        }

    }

    public int get_level(){
        return level;
    }

   async void Level3(){
        soundFX.Level3();
        await Task.Delay(1000);
        traverse();
        Obstacle1.SetActive(true);
      
        
    }

    async void Level4(){
        soundFX.Level4();
        await Task.Delay(1000);
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,false);
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);
        
    }

    async void Level5(){
        soundFX.Level5();
        await Task.Delay(1000);
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,false);
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);
        
    }


    async void traverse(){
        traversing = true;
        shoot_rotation = meHandle.GetRotation();
        soundFX.traverse();
        Ball.SetActive(false);
        meHandle.MoveToPosition(spawn,1.0f,true);
         Hole.SetActive(true);
         itHandle.SwitchTo(Hole,50.0f);
         
         
    
    }
    void reverse(){
        traversing = false;
         Ball.SetActive(true);
         Ball.transform.position = spawn;
         meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
         soundFX.readytoPlay();

         if(level ==  3){
            LevelComplete();
         }



    }
      private bool IsTurned(float start_rotation)
    {
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = meHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)rotate_result = 0 - rotate_result;
        int rotation_degree = 90;
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= rotation_degree && rotate_result <= (360 - rotation_degree);
    }

    void End(){
        Ball.SetActive(true);
        Hole.SetActive(true);
        meHandle.Free();
        itHandle.Free();
        //SoundFx.End();)
        }
}
