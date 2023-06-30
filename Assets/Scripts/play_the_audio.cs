using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class play_the_audio : MonoBehaviour
{
    AudioSource audioData;
    GameObject meHandle;
    float upperOffset = 4f;
    float lowerBounds = -12f;
    float normalizingFactor;

    void Start()
    {
        normalizingFactor = lowerBounds + upperOffset;
        meHandle = GameObject.FindGameObjectWithTag("MeHandleObject");
        audioData = GetComponent<AudioSource>();
        audioData.loop = true;
        audioData.Play(0);
        Debug.Log("started");
        
    }

    private void Update()
    {
        float Volume = 1f - Mathf.Min(Mathf.Max(0, Mathf.Min(meHandle.transform.position.z + upperOffset, 0) / (normalizingFactor)), 1f);
        //Debug.Log(Volume);
        audioData.volume = Volume;
    }


}