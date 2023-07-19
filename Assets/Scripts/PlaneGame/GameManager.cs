using DualPantoFramework;

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
        public static PantoHandle ItHandle;

        private int _rings;
        
        private async void Start()
        {
            _rings = GameObject.FindGameObjectsWithTag("ring").Length;
            soundEffects = GetComponent<SoundEffects>();
        }

        private Vector3 generateSpawnPosition()
        {
            Random rnd = new Random();
            return new Vector3((float)rnd.NextDouble() * 8f - 5f, 0f, -5f);
        }

        private async void Update()
        {
            _rings = GameObject.FindGameObjectsWithTag("ring").Length;

            if (_rings == 0 && !Plane.MeHandle.inTransition && (!ScoreManager.IsFreePlay && ((ScoreManager.LevelHasLives && !(ScoreManager.Lives <= 0)) || (!ScoreManager.LevelHasLives && ScoreManager.AimScoreStatic != ScoreManager.Score)) || ScoreManager.IsFreePlay))
            {
                GameObject newRing = Instantiate(ringPrefab,
                    generateSpawnPosition(),
                    ringPrefab.transform.rotation);
                
                ItHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
                await ItHandle.SwitchTo(newRing);
                _rings++;
                
            }
            
        }
    }
}