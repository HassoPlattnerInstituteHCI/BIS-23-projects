using UnityEngine;
using TMPro;
using SpeechIO;

namespace PlaneGame
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;

        public static int Score;
        public static int Misses;
        public static int Lives;
        public static int PointsLost;
        public static bool LevelHasLives;
        public static int AimScoreStatic;
        public static bool IsFreePlay;
        public static bool IsReduceScore;

        private SpeechIn speechIn;
        private SpeechOut speechOut;
        private SoundEffects soundEffects;

        public static int Level { get; private set; }

        public int aimScore;

        

        private float _gameOverTime = 0F;
        private string _levelMessage;
        private bool _objectiveCompleted = false;
        private int _missBuf = 0;
        
        // Start is called before the first frame update
        void Start()
        {
            Score = 0;
            Misses = 0;
            AimScoreStatic = aimScore;
            Lives = 3;
            PointsLost = 0;
            LevelHasLives = true;
            IsReduceScore = false;
            _levelMessage = "Finished tutorial.";
            
            speechOut = new SpeechOut();
            soundEffects = GameObject.FindObjectOfType<GameManager>().GetComponent<SoundEffects>();

            SetText();
        }

        // Update is called once per frame
        void Update()
        {
            SetText();
            
            if (LevelHasLives && Lives == 0 && !IsFreePlay && _gameOverTime == 0F)
            {
                _objectiveCompleted = Level == 0;
                _gameOverTime = Time.time;
                Debug.Log($"Completed level {Level}");
                soundEffects.SayText($"Congratulations! You finished the tutorial.");
            }
            else if (!LevelHasLives && Score == aimScore && !IsFreePlay && _gameOverTime == 0F)
            {
                _objectiveCompleted = true;
                _gameOverTime = Time.time;
                Debug.Log($"Completed level {Level}");
                soundEffects.CompletedLevel(Level);
            }
            else if (_gameOverTime != 0 && Time.time >= _gameOverTime + 5F)
            {
                _gameOverTime = 0F;
                Level++;
                Score = 0;
                Misses = 0;
                PointsLost = 0;
                AimScoreStatic = aimScore;
                _objectiveCompleted = false;

                if (!LevelHasLives)
                {
                    Lives = 0;
                }
                
                switch (Level)
                {
                    case 1:
                        soundEffects.SayText("Welcome to Level 1! Reach Score 3.");
                        LevelHasLives = false;
                        IsReduceScore = false;
                        _levelMessage = $"Reached target score: {aimScore}/{aimScore}";
                        break;
                    case 2:
                        soundEffects.SayText("Welcome to Level 2. Reach Score 3. Careful, there are negative points now, too.");
                        LevelHasLives = false;
                        IsReduceScore = true;
                        _levelMessage = $"Reached target score: {aimScore}/{aimScore}";
                        break;
                    default:
                        soundEffects.SayText("Welcome to the freeplay mode! Here you can collect points as long as you want. Have fun!");
                        LevelHasLives = false;
                        IsFreePlay = true;
                        IsReduceScore = false;
                        break;
                }
            }
        }

        async void SetText()
        {
            if (_gameOverTime != 0F)
            {
                if (_missBuf == 0)
                {
                    _missBuf = Misses;
                }

                if (_objectiveCompleted)
                {
                    _levelMessage = Level != 0 && !_levelMessage.Contains($"Level {Level} completed\n") 
                        ? $"Level {Level} completed\n" + _levelMessage 
                        : _levelMessage;
                    
                    if (!LevelHasLives)
                    {
                       _levelMessage = !_levelMessage.Contains($"\nTotal misses: {Misses}") 
                           ? _levelMessage + $"\nTotal misses: {Misses}" 
                           : _levelMessage;

                       if (IsReduceScore)
                       {
                           _levelMessage = !_levelMessage.Contains($"\nPoints lost: {PointsLost}")
                               ? _levelMessage + $"\nPoints lost: {PointsLost}"
                               : _levelMessage;
                       }
                    }
                    textField.text = _levelMessage;
                }
                else
                {
                    textField.text = "Game Over!";
                    soundEffects.SayText("Game Over!");
                }
            }
            else if (IsFreePlay)
            {
                _missBuf = _missBuf != 0 ? 0 : _missBuf;
                textField.text = $"Score: {Score}\nMisses: {Misses}\n[Free Play]";

            }
            else
            {
                _missBuf = _missBuf != 0 ? 0 : _missBuf;

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
}