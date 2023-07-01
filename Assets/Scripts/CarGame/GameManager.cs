using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CarMovement carMovement;
    private Driver driver;
    private Car car;
    private bool gameEnded = false;

    private void Start()
    {
        carMovement = FindObjectOfType<CarMovement>();
        driver = FindObjectOfType<Driver>();
        car = FindObjectOfType<Car>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGameWithCollision()
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

    public void ResumeGameAfterCollision()
    {
        if (gameEnded)
            return;

        Debug.Log("GameManager resumes game.");
        // Handle resuming the game after collision here
        // Example: Destroy the car and spawn a new one
        car.DestroyCar();
        carMovement.SpawnCar();
    }

    public void ActivatePlayer()
    {
        driver.ActivatePlayer();
    }
}
