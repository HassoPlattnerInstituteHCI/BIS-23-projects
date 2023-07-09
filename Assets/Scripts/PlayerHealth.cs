using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int enemyDamage;
    
    private SpeechOut speechOut;
    private bool exit = false;

    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
        speechOut.Speak("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {


        if(health <= 0 && !exit) {
            //exit
            speechOut.Speak("You Died");
            exit = true;
        }
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Enemy") {
            health -= enemyDamage;
        }
    }
}
