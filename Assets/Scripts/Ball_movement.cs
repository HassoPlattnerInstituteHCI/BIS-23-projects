using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Ball_movement : MonoBehaviour
{   
    //Declaring Variables 

    //customizable ones:
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject GameManager;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 1.8f;
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private float still_Speed = 0.1f;
    [SerializeField] private float min_Speed = 1f;

    [Header("Sounds")]
    [SerializeField] private AudioSource rail;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource wall;

    //helper variables:
    private bool isDragging;
    public bool inHole =  false;
    private UpperHandle upperHandle;
    private MeHandle mehandle;
    private bool is_shot = false;

    //Wall Childen
    private GameObject Wall_l;
    private GameObject Wall_r;
    private GameObject Wall_u;
    private GameObject Wall_d;

    
    //Hole Helper:
    float shoot_rotation;

    bool lvl1 = false;
    bool shot = false;

    // Start is called before the first frame update
    void Start()
    {   
        //get the me Handle
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        //disable Ball from rotating
        rb.freezeRotation = true;
        //start Me Handle at Ball position
        upperHandle.SwitchTo(gameObject,50.0f);

        Wall_l = Walls.transform.GetChild(3).gameObject;
        Wall_r = Walls.transform.GetChild(0).gameObject;
        Wall_u = Walls.transform.GetChild(2).gameObject;
        Wall_d = Walls.transform.GetChild(1).gameObject;

        if(GameManager.GetComponent<Gamecontroller_Golf>().get_level() == 1){
            lvl1 = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //Check for Player Input
        PlayerInput();

        if((rb.velocity.magnitude <= still_Speed) && is_shot){
            upperHandle.Free();
            is_shot = false;}

        if(shot && lvl1 && rb.velocity.magnitude <= 0.05f){
            //transform.position = new Vector3(3.0f,0.0f,-10.0f);
            GameManager.GetComponent<Gamecontroller_Golf>().LevelComplete();
            lvl1 =false;
        }
      
    }

    private bool IsReady()
    {
        // Check if ball is slow enough to be controlled
        return rb.velocity.magnitude <= still_Speed;
    }

    private bool IsTurned(float start_rotation)
    {
        // Calculate the difference between the current rotation of the UpperHandle and the start_rotation
        float rotate_result = upperHandle.GetRotation() - start_rotation;
        if (rotate_result < 0)rotate_result = 0 - rotate_result;
        int rotation_degree = 45;
        // Check if the rotation difference is within the allowed range (45 to 315 degrees)
        return rotate_result >= rotation_degree && rotate_result <= (360 - rotation_degree);
    }

    void Wall_Boxcollider(bool indicator){
        //turn the boxcollidor for all Walls off/ on depending on the indicator
        if(indicator){
            Wall_l.GetComponent<BoxCollider>().enabled = true;
            Wall_r.GetComponent<BoxCollider>().enabled = true;
            Wall_u.GetComponent<BoxCollider>().enabled = true;
            Wall_d.GetComponent<BoxCollider>().enabled = true;
        }
        else{
            Wall_l.GetComponent<BoxCollider>().enabled = false;
            Wall_r.GetComponent<BoxCollider>().enabled = false;
            Wall_u.GetComponent<BoxCollider>().enabled = false;
            Wall_d.GetComponent<BoxCollider>().enabled = false;
        }
        return;
    }

    Vector2 transform_Vector(Vector3 input_vector){
        //transforms a 3d Vector to a xz Vector
        Vector2 output_vector = new Vector2(input_vector.x,input_vector.z);
        return output_vector;
    }

    private void PlayerInput()
    {
        if (!IsReady())
            return;

        Vector2 handle_xz = transform_Vector(upperHandle.HandlePosition(transform.position));
        Vector2 ball_xz = transform_Vector(transform.position);

        // Get the distance of the UpperHandle from the ball's position
        float distance = Vector2.Distance(ball_xz, handle_xz);

        if (distance <= 0.2f && !isDragging)
            DragStart();
        else if (!IsTurned(shoot_rotation) &&  isDragging)
            DragChange(handle_xz);
        else if (isDragging){
            rail.Play(); 
            
            
            DragRelease(handle_xz);
            }
        
        
    }

    private void DragStart()
    {
        //allow the upper handle to move 
        upperHandle.Free();
        shoot_rotation = upperHandle.GetRotation();
        isDragging = true;
        lr.positionCount = 2;
    }

    private void DragChange(Vector2 handle_xz)
    {   Wall_Boxcollider(false);

        Vector2 ball_xz = transform_Vector(transform.position);
        Vector2 direction = ball_xz - handle_xz;

        Vector2 endline_vector = ball_xz + Vector2.ClampMagnitude((direction * power) / 2, maxPower / 2);
        Vector3 endline_position = new Vector3(endline_vector.x, transform.position.y + 0.9f, endline_vector.y);

        // Update the LineRenderer positions to visualize the shooting direction
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endline_position);
    }

    async void DragRelease(Vector2 handle_xz)
    {   
        Vector2 ball_xz = transform_Vector(transform.position);
        float distance = Vector2.Distance(ball_xz, handle_xz);
        isDragging = false;
        lr.positionCount = 0;
        

        if (distance < min_Speed)
            return;

        Vector2 direction = ball_xz - handle_xz;
        Vector2 velocity_speed = Vector2.ClampMagnitude(direction * power, maxPower);
        Vector3 result_velocity = new Vector3(velocity_speed.x, 0.0f, velocity_speed.y);

        // Switch the UpperHandle to control the ball and set its velocity based on the shooting direction
        Wall_Boxcollider(true);

        await upperHandle.SwitchTo(gameObject,50.0f);
        rb.velocity = result_velocity;
        // Wait until the ball's velocity magnitude falls below the still_Speed threshold
        upperHandle.SwitchTo(gameObject, 50.0f);
        is_shot = true;
        shot = true;
        // Release the UpperHandle from controlling the ball
        
    }

    private void CheckWinState(Collider other){
        if(inHole) {
            return;
        }
        if(rb.velocity.magnitude <= maxGoalSpeed) {
            win.Play();
            inHole = true;
            
            rb.velocity = Vector3.zero;
            transform.position = other.transform.position;
            //gameObject.SetActive(false);
            GameManager.GetComponent<Gamecontroller_Golf>().LevelComplete();
            }
        
            //LevelComplete
    }
    


    private void OnTriggerEnter(Collider other){
        if(other.tag == "Goal") {
        CheckWinState(other);}

        if(other.tag == "Audio_Wall" && rb.velocity.magnitude > still_Speed) {
        wall.Play();}
    }

}