using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarGame
{
    public class GameManager : MonoBehaviour
    {
        private CarMovement carMovement;
        private Driver driver;
        private Car car;
        private bool gameEnded = false;
        private Car carComponent;
        private CarMovement carMovementComponent;

        private void Start()
        {
            carMovement = FindObjectOfType<CarMovement>();
            driver = FindObjectOfType<Driver>();
            car = FindObjectOfType<Car>();
            carComponent = GetComponent<Car>();
            carMovementComponent = GetComponent<CarMovement>();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void EndGameWithCollision(GameObject collisionCar)
        {
            if (gameEnded)
                return;

            Debug.Log("GameManager ends game.");
            gameEnded = true;

            // Handle game over logic here
            // Example: Destroy the car and stop the game
            car.DestroyCar();
            carMovement.startedGame = false;
        }

        public void ResumeGameAfterCollision(GameObject collisionCar)
        {
            if (gameEnded)
                return;

            Debug.Log("GameManager resumes game.");
            // Handle resuming the game after collision here
            // Example: Destroy the car and spawn a new one
            collisionCar.GetComponent<Car>().DestroyCar();
            carMovementComponent.SpawnCar();
            Debug.Log("in Resume game after collsion we respawned a new car.");
        }

        public void ActivatePlayer()
        {
            driver.ActivatePlayer();
        }
    }
}