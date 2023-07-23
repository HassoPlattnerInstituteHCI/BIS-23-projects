using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class SoundEffects : MonoBehaviour
{
    public AudioClip coinSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void coinCollected()
    {
        audioSource.PlayOneShot(coinSound);
    }
}
