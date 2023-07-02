using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;

public class car : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 breapoint = new Vector3(-8, 0, 4);
    public float handleSpeed = 10f;

    private PantoHandle lowerHandle;
    private GameObject carObject;
    private bool isMoving;
    private bool backwards = false;


    Game game;
    async void Start()
    {
        carObject = this.gameObject;
        backwards = false;
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();        

        // await lowerHandle.SwitchTo(carObject, 10f);


        // transform.position = carObject.transform.position;
        // // await GameObject.Find("Panto").GetComponent<UpperHandle>().MoveToPosition(new Vector3(0, 0, -10), handleSpeed);
        // game = GetComponent<Game>();
        // carObject.transform.position = startPosition;
        // await lowerHandle.SwitchTo(carObject, 10f);
        // await lowerHandle.MoveToPosition(breapoint, handleSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        //if (carObject.transform.position.x < breapoint.x)
        //{
        //    carObject.transform.position = new Vector3(carObject.transform.position.x + 0.0001f, carObject.transform.position.y, carObject.transform.position.z);
        //}
        //else
        //{
        //    carObject.transform.position = startPosition;
        //}

    }

    // move car to the right on physics update
    async void FixedUpdate()
    {
        if (!isMoving)
        {
            isMoving = true;
            if (backwards)
            {
                await lowerHandle.MoveToPosition(breapoint, handleSpeed, true);
                backwards = false;

            }
            else
            {
                await lowerHandle.MoveToPosition(startPosition, handleSpeed, true);
                backwards = true;
            }
            isMoving = false;
        }



        // if (carObject.transform.position.x < breapoint.x)
        // {           
        //     await lowerHandle.MoveToPosition(new Vector3(transform.position.x + 0.0001f, transform.position.y, transform.position.z), handleSpeed);
        //     // transform.position = new Vector3(transform.position.x + 0.0001f, transform.position.y, transform.position.z);
        // }
        // else
        // {
        //     await lowerHandle.MoveToPosition(startPosition, 10f);
        //     carObject.transform.position = startPosition;
        // }
    }
}
