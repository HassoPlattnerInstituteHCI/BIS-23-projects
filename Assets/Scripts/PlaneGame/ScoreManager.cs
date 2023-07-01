using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using TMPro;

using UnityEngine.Serialization;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    public static int Score;
    public static int Misses;
    public static int Lives;
    public static bool LevelHasLives;
    public static int AimScoreStatic;
    public static bool IsLastLevel;
    public static int Level { get; private set; }
    
    public int aimScore;

    

    private float _gameOverTime = 0F;
    

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Misses = 0;
        AimScoreStatic = aimScore;
        Lives = 3;
        LevelHasLives = true;
        
        
        
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelHasLives)
        {
            Lives = 0;
        }
        
        SetText();
        if (LevelHasLives && Lives == 0 && _gameOverTime == 0F)
        {
            _gameOverTime = Time.time;
            Debug.Log($"Completed level {Level}");
        }
        else if (!LevelHasLives && Score == aimScore && _gameOverTime == 0F)
        {
            _gameOverTime = Time.time;
            Debug.Log($"Completed level {Level}");
        }
        else if (_gameOverTime != 0 && Time.time >= _gameOverTime + 5F)
        {
            _gameOverTime = 0F;
            
            Level++;
            
            Score = 0;
            Misses = 0;
            AimScoreStatic = aimScore;
            
            switch (Level)
            {
                case  0:
                    Lives = 3;
                    LevelHasLives = true;
                    break;
                case 1:
                    LevelHasLives = false;
                    break;
                case 2:
                    LevelHasLives = false;
                    break;
                default:
                    LevelHasLives = false;
                    IsLastLevel = true;
                    break;
            }

            if (!LevelHasLives)
            {
                Lives = 0;
            }
        }
    }

    void SetText()
    {
        if (_gameOverTime != 0F)
        {
            textField.text = "Game Over!";
        }
        else if (IsLastLevel)
        {
            textField.text = $"Score: {Score}/{aimScore}\nMisses: {Misses}\n[Free Play]";
        }
        else
        {
            switch (Level)
            {
                case 0:
                    string life_string = Lives == 1 ? "life" : "lives";
                    textField.text = $"{Lives} {life_string} left\nScore: {Score}\n[Tutorial mode]";
                    break;
                case 1:
                    textField.text = $"Score: {Score}/{aimScore}\nLevel {Level}";
                    break;
                case 2:
                    textField.text = $"Score: {Score}/{aimScore}\nMisses: {Misses}\nLevel {Level}";
                    break;
            } 
        }
    }
}
