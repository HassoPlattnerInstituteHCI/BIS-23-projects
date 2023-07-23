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
    [SerializeField] private GameObject SpawnGuy;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    [SerializeField] private GameObject Wall5;

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
        SpawnGuy.transform.position = spawn;
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
        await Task.Delay(5000);
        meHandle.Free();
        meHandle.SwitchTo(SpawnGuy,30f);
        movement.lvl1 = true;
        await Task.Delay(2000);
        Ball.SetActive(true);
        movement.ready = true;
        meHandle.Free();

    }

    async void Level2(){
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,30f);
        
       
        meHandle.SwitchTo(SpawnGuy,30f);
        await Task.Delay(2000);
        Ball.SetActive(true);
        movement.ready = true;
        //meHandle.Free();
        
    }

    // async void Level3(){
    //     soundFX.Level3();
    //     await Task.Delay(1000);
    //     meHandle.Free();
    //     Obstacle1.SetActive(true);
      
        
    // }

    async void Level4(){
        meHandle.SwitchTo(SpawnGuy,30f);
        itHandle.SwitchTo(Hole,30f);
        await Task.Delay(2000);
        movement.ready = true;
        Ball.SetActive(true);
        
    }

    async void Level5(){
        meHandle.SwitchTo(SpawnGuy,30f);
        itHandle.SwitchTo(Hole,30f);
        await Task.Delay(2000);
        movement.ready = true;
        Ball.SetActive(true);
        
        
    }

   async void End(){
        Ball.SetActive(false);
        
        meHandle.Free();
        itHandle.Free();
        await Task.Delay(3000);
        soundFX.finish();


        }

    public void LevelComplete(){
        Ball.SetActive(false);
        Ball.transform.position = spawn;
        meHandle.SwitchTo(SpawnGuy,50f);
        level++;
        soundFX.Level_select(level);
        switch (level){
            case 1:
            Level1();
            break;

            case 2:
            Level2();
            movement.inHole = false;
            break;

            case 3:
            Wall3.SetActive(true);
            traverser.TraverseSetup(3);
            break;

            case 4:
            Wall3.SetActive(false);
            Wall4.SetActive(true);
            traverser.TraverseSetup(4);
            break;

            case 5:
            Wall4.SetActive(false);
            Wall5.SetActive(true);
            traverser.TraverseSetup(5);
            break;

            default:
            Wall5.SetActive(false);
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
            LevelComplete();
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
        movement.inHole=false;

    }

    public int get_level(){
        return level;
    }

    
}