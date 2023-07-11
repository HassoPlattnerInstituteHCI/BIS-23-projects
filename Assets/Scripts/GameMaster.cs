using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class GameMaster : MonoBehaviour
{   
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Hole;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject CollisionHelper;
    [SerializeField] private GameObject Obstacle1;

    private PantoHandle itHandle;
    private PantoHandle meHandle;
    private GameObject MeHandleGodObject;
    private AudioFX soundFX;
    private Traverse traverser;
    private Movement movement;
    private int level = 0;
    private Vector3 spawn = new Vector3(3.0f,0.0f,-10.0f);
    private Vector3 hole = new Vector3(-3.5f,0.35f,-10.0f);
    bool traversing = false;
    float shoot_rotation;
     
    // Start is called before the first frame update
    void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        soundFX = gameObject.GetComponent<AudioFX>();
        traverser = gameObject.GetComponent<Traverse>();
        movement = Ball.GetComponent<Movement>();
        
        LevelComplete();
    }

    // Update is called once per frame
    void FixedUpdate(){   
    }

    async void Level1(){
        Ball.transform.position = spawn;
        soundFX.Level1();
        await meHandle.MoveToPosition(spawn,10f,false);
        Ball.SetActive(true);
        meHandle.Free();

    }

    async void Level2(){
        soundFX.Level2();
        await Task.Delay(1000);
        Ball.transform.position = spawn;
        Ball.SetActive(true);
        meHandle.Free();
        Obstacle1.SetActive(true);
        
    }

    async void Level3(){
        soundFX.Level3();
        await Task.Delay(1000);
        meHandle.Free();
        Obstacle1.SetActive(true);
      
        
    }

    async void Level4(){
        soundFX.Level4();
        await Task.Delay(1000);
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.MoveToPosition(Hole.transform.position,50.0f,false);
        
    }

    async void Level5(){
        soundFX.Level5();
        await Task.Delay(1000);
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.MoveToPosition(Hole.transform.position,50.0f,false);
        
    }

    void End(){
        Ball.SetActive(true);
        Hole.SetActive(true);
        meHandle.Free();
        itHandle.Free();
        //SoundFx.End();
        }

    public void LevelComplete(){
        level++;
        switch (level){
            case 1:
            Level1();
            break;

            case 2:
            Level2();
            movement.inHole = false;
            break;

            case 3:
            traverser.TraverseSetup(3);
            break;

            case 4:
            traverser.TraverseSetup(4);
            break;

            case 5:
            traverser.TraverseSetup(5);
            break;

            default:
            End();
            break;
        }

    }

    public void TraverseComplete(){
        switch (level){
            case 2:
            Level2();
            break;  

            case 3:
            Level3();
            break;

            case 4:
            LevelComplete();
            break;

            case 5:
            Level5();
            break;

            default:
            End();
            break;
        }
        movement.inHole=false;

    }

    public int get_level(){
        return level;
    }

    
}