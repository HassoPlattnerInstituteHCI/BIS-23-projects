using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    public static int score;

    public static int misses;

    public static int lives;

    public int level;
    //public int lives; //possible switch to a life-loss based system
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        misses = 0;

        switch (level)
        {
            case  0:
                lives = 3;
                break;
            case 1:
                lives = 0;
                break;
            case 2:
                lives = 0;
                break;
        }
        
        setText();
    }

    // Update is called once per frame
    void Update()
    {
        setText();
        if (lives == 0)
        {
            textField.text = "Game Over!";
            PlaneGame.GameManager.running = false;

            Task.Delay(5000);

            level++;
            PlaneGame.GameManager.running = true;

        }
    }

    void setText()
    {
        switch (level)
        {
            case 0:
                textField.text = $"Tutorial mode\n{lives} lives left\nScore: {score}";
                break;
            case 1:
                textField.text = $"Score: {score}";
                break;
            case 2:
                textField.text = $"Score: {score}\nMisses: {misses}";
                break;
        }
        
    }
}
