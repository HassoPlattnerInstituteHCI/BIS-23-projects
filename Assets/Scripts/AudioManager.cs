using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audiQuellePrefab;
    public AudioClip Intro1;
    public AudioClip Track1;
    public AudioClip Throw1;
    public AudioClip Slash1;
    public AudioClip Cut1;
    public AudioClip Cut2;
    public AudioClip Success;
    public AudioClip missedFruit;
    public AudioClip BombShot;
    public AudioClip BombSizzeling;
    public AudioClip BombExplode; //Todo das Ding einfügen


    // Start is called before the first frame update
    void Start()
    {
        //Intro = Resources.Load<AudioClip>("Assets/Audio/Intro1");
        //playIntro();
        //GameObject introductionManager = GameObject.Find("IntroductionManager");
        //introductionManager.SetActive(false);
        //playSound("Intro1");
        //WaitForSeconds(5);
        //introductionManager.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound(string type)
    {
        GameObject ton = null;
        ton = Instantiate(audiQuellePrefab);
        AudioSource tonScript = ton.GetComponent<AudioSource>();
        switch (type)
        {
            case "Intro1":
                tonScript.clip = Intro1;
                tonScript.volume = 0.5f;
                //WaitForSeconds(Intro1.length);
                break;
            case "Track1":
                tonScript.clip = Track1;
                tonScript.loop = true;
                break;
            case "Throw1":
                tonScript.clip = Throw1;
                break;
            case "Slash1":
                tonScript.clip = Slash1;
                break;
            case "Cut1":
                tonScript.clip = Cut1;
                break;
            case "Cut2":
                tonScript.clip = Cut2;
                break;
            case "Success":
                tonScript.clip = Success;
                break;
            case "missedFruit":
                tonScript.clip = missedFruit;
                break;
            case "BombShot":
                tonScript.clip = BombShot;
                break;
            case "BombSizzeling":
                tonScript.clip = BombSizzeling;
                break;
            case "BombExplode":
                tonScript.clip = BombExplode;
                break;
        }

            tonScript.Play();
            DontDestroyOnLoad(ton);
            if (type != "Track1"&&type!="BombSizzeling")
        {
            //WaitForSeconds(tonScript.clip.length);
            Destroy(ton, tonScript.clip.length);
        }
        return;
        }

    void playMusic()
        {
            GameObject ton = null;
            ton = Instantiate(audiQuellePrefab);
            AudioSource tonScript = ton.GetComponent<AudioSource>();
            tonScript.clip = Intro1;
            tonScript.Play();
            DontDestroyOnLoad(ton);
        }
}