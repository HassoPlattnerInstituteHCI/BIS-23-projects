using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    AudioSource audioData;
    GameObject controlIndicator;
    float maxVolumeScaling = 0.3f;
    float upperOffset = -8.5f;
    float lowerBounds = -15f;

    void Start()
    {
        
        controlIndicator = GameObject.Find("SoundIndicator");
        audioData = GetComponent<AudioSource>();
        audioData.loop = true;
        audioData.Play(0);
        Debug.Log("started");

    }

    private void Update()
    {
        float Volume = (1- (controlIndicator.transform.position.z+upperOffset)/(lowerBounds+upperOffset)) * maxVolumeScaling;
        //Debug.Log(Volume);
        audioData.volume = Volume;
    }
}
