using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

namespace CarGame
{
    public class Driver : MonoBehaviour
    {
        private UpperHandle upperHandle;
        private Rigidbody playerRb;
        public GameManager gameManager;
        bool endGameWithCollision; //variation of ending the game if a car is hit or letting a new car respawn

        void Start()
        {
            endGameWithCollision = false;
            playerRb = GetComponent<Rigidbody>();
            gameManager = FindObjectOfType<GameManager>();
            //PantoMovement();
        }

        void Update()
        {
            if (!GameObject.FindObjectOfType<CarMovement>().startedGame) return;
        }

        void FixedUpdate()
        {
            transform.position = (upperHandle.HandlePosition(transform.position));
            transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
        }

        void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;

            if (other.GetComponent<Car>() != null)
            {
                // The GameObject is of type Car
                Debug.Log("Collision with Car. End Game with collision is: " + endGameWithCollision);
                if (endGameWithCollision)
                {
                    //It handle freezes, nothing happens anymore. Game over.
                    gameManager.EndGameWithCollision(other);
                }
                else
                {
                    //resume as if nothing has happend => respawn a new car
                    gameManager.ResumeGameAfterCollision(other);
                }
            }
            else
            {
                // The GameObject is of an unknown type or doesn't have any specific component
                Debug.Log("Collision with Unknown GameObject");
                return;
            }
        }

        public async Task ActivatePlayer()
        {
            upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
            //await upperHandle.SwitchTo(gameObject);
            upperHandle.FreeRotation();
            upperHandle.Free();
        }
    }
}


