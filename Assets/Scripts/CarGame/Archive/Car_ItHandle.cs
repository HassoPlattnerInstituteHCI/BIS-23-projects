using UnityEngine;
using DualPantoFramework;
using System;

public class Car_ItHandle : MonoBehaviour
{
    PantoHandle lowerHandle;
    int count = 0;
    bool activeCar = false;
    bool drivingCar = false;
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    }

    void Update()
    {
        if(!activeCar && count <= 5)
        {
            SpawnCar();
        }
        else
        {
            lowerHandle.Freeze();
        }
    }

    // moves the handle to a random position at the top of the road
    async void SpawnCar()
    {
        //spawn at x \in {-3, 7}, y = 10 (?), z = -5

        var rand = new System.Random();
        float x = rand.Next(-3, 7);

        Vector3 newPosition = new Vector3(x, 10f, -5f);

        activeCar = true;
        await lowerHandle.MoveToPosition(newPosition);

        DriveCar(x);
    }

    // drives car on the z-axe towards the user
    async void DriveCar(float x)
    {
        Vector3 endOfRoad = new Vector3(x, 10f, -11f);
        drivingCar = true;
        await lowerHandle.MoveToPosition(endOfRoad, 1f, false);

        activeCar = false;
        drivingCar = false;
        count++;
    }
}