using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using static GameManager;

public class PacMan : MonoBehaviour
{
    private SoundEffects soundEffects;

    void Start()
    {
        soundEffects = GetComponent<SoundEffects>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            soundEffects.coinCollected();
            Destroy(other.gameObject);
            //RefreshClosestCoinTEST();
        }
    }
}
