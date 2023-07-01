using UnityEngine;
using DualPantoFramework;
using System;
using System.Threading.Tasks;
using SpeechIO;

public class CarMovement : MonoBehaviour
{
    //PantoHandle lowerHandle;
    public GameObject carPrefab;
    public bool startedGame;
    private SpeechOut speechOut;
    public int carCount; //room to use this to speed up car 
    public bool carSpawning = false;
    Vector3 defaultPosition = new Vector3(0f, 0f, -5f);

    private async void Start()
    {
        StartGame();
    }

    async void StartGame()
    {
        // GameObject car = Instantiate(carPrefab, ResetObject(), carPrefab.transform.rotation);
        // GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(car);
        Debug.Log("First Car is spawning: " + carSpawning);

        await SpawnCar();

        Debug.Log("After first Car has spawned: " + carSpawning);

        GameObject.FindObjectOfType<Driver>().ActivatePlayer();

        startedGame = true;
    }

    private async void FixedUpdate()
    {
        if (!startedGame) 
        {
            Debug.Log("Restarting//game is offline.");
            return;
        }
        else
        {
            Debug.Log("Game is online.");
        }
    }

    private async void Update()
    {
        if(!startedGame) return;

        Debug.Log("Car is spawning: " + carSpawning);

        Car thisCar = null;
        thisCar = FindObjectOfType<Car>();
        if(thisCar == null)
        {
            return;
        }

        if (!thisCar.getCar())
        {
            Destroy(thisCar.gameObject);
            await GameObject.Find("Panto").GetComponent<LowerHandle>().MoveToPosition(defaultPosition, 50f, false);

            carCount++;

            await Task.Delay(2000); // Wait for 2 seconds

            await SpawnCar();
        }
    }
    /*private void Update()
    {
        if (!startedGame) return;
    
        GameObject thisCar = GameObject.FindObjectOfType<Car>();
        if(!thisCar.getCar())
        {
            GameObject.Destroy(thisCar);
            carCount++;
            SpawnCar();
        }

        // if(!GameObject.Find("Car").GetComponent<Car>().getCar())
        // {
        //     GameObject.Find("Panto").GetComponent<LowerHandle>().Free();
        //     carCount++;
        //     SpawnCar();
        // }
        

        // Move the object along the Z-axis
        //transform.Translate((-Vector3.forward) * movementSpeed * Time.deltaTime);
    }*/

    private Vector3 ResetObject()
    {
        // Place the object at a random point in the upper frame
        float randomX = new System.Random().Next(-3, 7);
        float y = 0f;
        float z = -5f;
        Vector3 position = new Vector3(randomX, y, z);
        return position;
    }

    public async Task SpawnCar()
    {
        Debug.Log("--------------------Entered SpawnCar() method. carSpawning is: " + carSpawning);
        if(carSpawning)
        {
            return;
        }
        if(GameObject.Find("Panto").GetComponent<LowerHandle>().isFrozen)
        {
            GameObject.Find("Panto").GetComponent<LowerHandle>().Free();
            Debug.Log("freeeeed handle after freeze.");
        }

        carSpawning = true;
        Vector3 spawnpoint = ResetObject();
        Debug.Log("++++++++++++++++++++ ResetObject() gave position: " + spawnpoint);

        await GameObject.Find("Panto").GetComponent<LowerHandle>().MoveToPosition(spawnpoint, 60f, true);
        Debug.Log("++++++++++++++++++++ It handle is at position: " + GameObject.Find("Panto").GetComponent<Transform>().position);

        GameObject car = Instantiate(carPrefab, spawnpoint, carPrefab.transform.rotation);
        carSpawning = false;
        GameObject.FindObjectOfType<Car>().ActivateCar();

        await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(car);
    }
}