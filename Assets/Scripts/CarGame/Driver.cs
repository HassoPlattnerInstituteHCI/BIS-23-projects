using UnityEngine;
using DualPantoFramework;
using System;
using System.Threading.Tasks;

public class Driver : MonoBehaviour 
{
    private UpperHandle upperHandle;
    private Rigidbody playerRb;
 
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //PantoMovement();
    }

    void Update()
    {
        if(!GameObject.FindObjectOfType<CarMovement>().startedGame) return;
    }

    void FixedUpdate()
    {
        transform.position = (upperHandle.HandlePosition(transform.position));
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Debug.Log("Collision!! TODO: restart here");
        //TODO end game with collision
    }

    public async Task ActivatePlayer()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        //await upperHandle.SwitchTo(gameObject);
        upperHandle.FreeRotation();
        upperHandle.Free();
    }
}


