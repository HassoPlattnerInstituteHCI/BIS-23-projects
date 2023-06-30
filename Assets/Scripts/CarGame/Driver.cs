using UnityEngine;
using DualPantoFramework;
using System;
using System.Threading.Tasks;

public class Driver : MonoBehaviour 
{
    private UpperHandle upperHandle;
 


    void Update()
    {
        if(!GameObject.FindObjectOfType<CarMovement>().startedGame) return;

    }

    
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        //TODO end game with collision
    }

    public async Task ActivatePlayer()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        await upperHandle.SwitchTo(gameObject);
        upperHandle.FreeRotation();
    }

}


