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



    void FixedUpdate()
    {
        if (carDriving)
        {
            // Vector3 lookDirection = -transform.forward;
            // carColor.AddForce(lookDirection.normalized * movementSpeed);

            if (transform.position.z <= targetZ)
            {
                if(carCount >= 5)
                {
                    movementSpeed = 5f;
                }
                else if(carCount >= 10)
                {
                    movementSpeed = 10f;
                }
            }
            else
            {
                GameObject.FindObjectOfType<CarMovement>().SpawnCar();
            }
        }
    }

    public void DriveCar(int carCount)
    {
        if (transform.position.z <= targetZ)
        {
            if(carCount >= 5)
            {
                movementSpeed = 5f;
            }
            else if(carCount >= 10)
            {
                movementSpeed = 10f;
            }
        }
        else
        {
            GameObject.FindObjectOfType<CarMovement>().SpawnCar();
        }
    }

}
