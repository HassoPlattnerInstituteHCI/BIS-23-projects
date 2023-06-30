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
    public int carCount;

    private void Start()
    {
        StartGame();
    }

    async void StartGame()
    {
        // GameObject car = Instantiate(carPrefab, ResetObject(), carPrefab.transform.rotation);
        // GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(car);

        SpawnCar();
        await GameObject.FindObjectOfType<Car>().ActivateCar();

        
        //TODO write player as Driver
        //GameObject.FindObjectOfType<Driver>().ActivatePlayer();

        startedGame = true;
    }

    private void Update()
    {
        if (!startedGame) return;
    
        if(!GameObject.FindObjectOfType<Car>().getCar())
        {
            GameObject.Find("Panto").GetComponent<LowerHandle>().Free();
            carCount++;
            SpawnCar();
        }
        

        // Move the object along the Z-axis
        //transform.Translate((-Vector3.forward) * movementSpeed * Time.deltaTime);
    }

    private Vector3 ResetObject()
    {
        // Place the object at a random point in the upper frame
        float randomX = new System.Random().Next(-3, 7);
        float y = 0f;
        float z = -5f;
        Vector3 position = new Vector3(randomX, y, z);
        return position;
    }

    public 
    async Task SpawnCar()
    {
        GameObject car = Instantiate(carPrefab, ResetObject(), carPrefab.transform.rotation);
        await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(car);
        
        //active car
        GameObject.FindObjectOfType<Car>().ActivateCar();
        await speechOut.Speak("Caution! This car is approaching you.");
    }
}