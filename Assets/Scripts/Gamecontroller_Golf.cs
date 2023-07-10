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

    private PantoHandle itHandle;
    private PantoHandle meHandle;
    private AudioFX soundFX;
    private Ball_movement Movement;
    private int level = 0;
    private Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        // meHandleGodObject = GameObject.Find("Panto").GetComponent<MeHandleGodObject>();
        soundFX = gameObject.GetComponent<AudioFX>();
        Movement = Ball.GetComponent<Ball_movement>();

        spawn = new Vector3(3.0f,0.0f,-10.0f);
        Ball.transform.position = spawn;
        
        Level1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Level1(){
        level = 1;
        soundFX.Level1();
        Ball.SetActive(true);

    }

    void Level2(){
        soundFX.Level2();
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

    void Level3(){
        soundFX.Level3();
        Ball.SetActive(false);
        Cube.SetActive(true);
        meHandle.MoveToPosition(spawn,1.0f,true);        
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);

    }

    void Level4(){
        soundFX.Level4();
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,false);
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);
        
    }

    void Level5(){
        soundFX.Level5();
        Ball.transform.position = spawn;
        meHandle.MoveToPosition(Ball.transform.position,1.0f,false);
        meHandle.MoveToPosition(Ball.transform.position,1.0f,true);
        Hole.SetActive(true);
        itHandle.SwitchTo(Hole,50.0f);
        
    }

    void End(){
        Ball.SetActive(false);
        Hole.SetActive(false);
        //SoundFx.End();)
        }
}
