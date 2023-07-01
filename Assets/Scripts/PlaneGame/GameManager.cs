using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

using Random = System.Random;


namespace PlaneGame
{
    public class GameManager : MonoBehaviour
    {
        //plane prefab might be unneccessary?
        //public GameObject planePrefab;
        public GameObject ringPrefab;

        private int _rings;
        
        private void Start()
        {
            _rings = GameObject.FindGameObjectsWithTag("ring").Length;
        }

        private void Update()
        {
            Random rnd = new Random();

            _rings = GameObject.FindGameObjectsWithTag("ring").Length;

            if (_rings == 0 && ((ScoreManager.Level == 0 && !(ScoreManager.Lives <= 0)) || (ScoreManager.Level > 0 && ScoreManager.AimScoreStatic != ScoreManager.Score)))
            {
                Instantiate(ringPrefab,
                    new Vector3(rnd.Next(-10, 11), ringPrefab.transform.position.y, ringPrefab.transform.position.z),
                    ringPrefab.transform.rotation);
                _rings++;
            }
            
        }
    }
}