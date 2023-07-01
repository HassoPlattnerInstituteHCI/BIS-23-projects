using System;

using UnityEngine;
using UnityEngine.UIElements;

using Random = System.Random;


namespace PlaneGame
{
    public class GameManager : MonoBehaviour
    {
        public GameObject planePrefab;
        public GameObject ringPrefab;

        private int _rings = 1;

        private void Update()
        {
            Random rnd = new Random();

            _rings = GameObject.FindGameObjectsWithTag("ring").Length;

            if (_rings == 0)
            {
                Instantiate(ringPrefab,
                    new Vector3(rnd.Next(-10, 11), ringPrefab.transform.position.y, ringPrefab.transform.position.z),
                    ringPrefab.transform.rotation);
                _rings++;
            }
        }
    }
}