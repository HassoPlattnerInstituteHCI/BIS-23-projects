using UnityEngine;
using System.Threading.Tasks;
using DualPantoFramework;

namespace CarGame
{
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
            Debug.Log("Activated car.");
        }

        public async Task DeactivateCar()
        {
            carDriving = false;
        }

        public async Task DestroyCar()
        {
            Debug.Log("Deactivating car. Life about to end.");

            // Freeze the lower handle
            GameObject.Find("Panto").GetComponent<LowerHandle>().Freeze();
            Debug.Log("Lower handle frozen.");

            // Sleep the car rigidbody
            carColor.Sleep();

            // Destroy the car object
            Destroy(gameObject);
            Debug.Log("Destroyed car gameObject.");
        }

        public bool getCar()
        {
            return carDriving;
        }
        
        

        void FixedUpdate()
        {
            if(!carDriving) return;
            else
            {
                if(transform.position.z >= targetZ)
                {
                    //Vector3 lookDirection = -Vector3.forward;
                    carColor.AddForce(-Vector3.forward * movementSpeed); 

                    //transform.Translate(lookDirection * movementSpeed * Time.deltaTime);
                }
                else if(transform.position.z <= targetZ)
                {
                    carColor.Sleep();
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

}
