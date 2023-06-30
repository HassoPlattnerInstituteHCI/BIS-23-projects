using UnityEngine;
using System.Threading.Tasks;
using DualPantoFramework;

public class Car : MonoBehaviour
{
    public float movementSpeed = 1f;
    private Rigidbody carColor;
    private GameObject driver;
    private bool carDriving = false;
    private float targetZ = -11f; // Target Z-coordinate
    int carCount = 0; //count to alter speed of car 


    void Start()
    {
        carColor = GetComponent<Rigidbody>();
        driver = GameObject.Find("Driver");
    }

    void Update()
    {

    }
    
    public async Task ActivateCar()
    {
        carDriving = true;
    }

    public async Task DeactivateCar()
    {
        carDriving = false;
    }

    public bool getCar()
    {
        return carDriving;
    }


    void FixedUpdate()
    {
        if (carDriving)
        {
            if(transform.position.z >= targetZ)
            {
                Vector3 lookDirection = -Vector3.forward;
                carColor.AddForce(lookDirection.normalized * movementSpeed); 
                transform.Translate(lookDirection * movementSpeed * Time.deltaTime);
            }
            else
            {
                DeactivateCar();
            }
        }
    }

    // public void DriveCar(int carCount)
    // {
    //     if (transform.position.z <= targetZ)
    //     {
    //         if(carCount >= 5)
    //         {
    //             movementSpeed = 5f;
    //         }
    //         else if(carCount >= 10)
    //         {
    //             movementSpeed = 10f;
    //         }
    //     }
    //     else
    //     {
    //         GameObject.FindObjectOfType<CarMovement>().SpawnCar();
    //     }
    // }

}
