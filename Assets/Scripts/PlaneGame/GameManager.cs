using UnityEngine;
using Random = System.Random;
using SpeechIO;


namespace PlaneGame
{
    public class GameManager : MonoBehaviour
    {
        //plane prefab might be unneccessary?
        //public GameObject planePrefab;
        public GameObject ringPrefab;
        public SoundEffects soundEffects;

        private int _rings;
        
        private void Start()
        {
            _rings = GameObject.FindGameObjectsWithTag("ring").Length;
            soundEffects = GetComponent<SoundEffects>();
        }

        private void Update()
        {
            Random rnd = new Random();

            _rings = GameObject.FindGameObjectsWithTag("ring").Length;

            if (_rings == 0 && (!ScoreManager.IsFreePlay && ((ScoreManager.LevelHasLives && !(ScoreManager.Lives <= 0)) || (!ScoreManager.LevelHasLives && ScoreManager.AimScoreStatic != ScoreManager.Score)) || ScoreManager.IsFreePlay))
            {
                Instantiate(ringPrefab,
                    new Vector3(rnd.Next(-5, 8), ringPrefab.transform.position.y, ringPrefab.transform.position.z),
                    ringPrefab.transform.rotation);
                _rings++;
            }
            
        }
    }
}