using System.Threading.Tasks;
using DualPantoFramework;
using UnityEngine;
using SpeechIO;

namespace PlaneGame
{
    public class Ring : MonoBehaviour
    {
        private SpeechIn speechIn;
        private SpeechOut speechOut;
        private SoundEffects soundEffects;


        private async void Start()
        {
            speechOut = new SpeechOut();
            soundEffects = GameObject.FindObjectOfType<GameManager>().GetComponent<SoundEffects>();

            Debug.LogError("RING CREATED");
            //_itHandle.SwitchTo(gameObject);
        }

        private async void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.025F);

            if (transform.position.z <= -16.5)
            {
                Destroy(gameObject);
                ScoreManager.Misses++;
                soundEffects.MissedRing();

                if (ScoreManager.LevelHasLives)
                {
                    ScoreManager.Lives--;
                    soundEffects.AnnounceLives(ScoreManager.Lives);
                }
                
                if (!ScoreManager.LevelHasLives && ScoreManager.Score > 0 && ScoreManager.IsReduceScore)
                {
                    ScoreManager.Score--;
                    ScoreManager.PointsLost++;
                    soundEffects.AnnouncePoints(ScoreManager.Score, ScoreManager.Misses);
                }
                else if(!ScoreManager.LevelHasLives && ScoreManager.Score > 0 && ScoreManager.IsReduceScore)
                {
                    soundEffects.AnnouncePoints(0, ScoreManager.Misses);
                }

                if(ScoreManager.IsFreePlay)
                {
                    soundEffects.AnnouncePoints(ScoreManager.Score, ScoreManager.Misses);
                }
            }
        }
    }
}