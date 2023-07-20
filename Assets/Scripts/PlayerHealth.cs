using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using UnityEngine.SceneManagement;

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
            speechOut.Speak("You Died, Try Again!");
            if(SceneManager.GetActiveScene().name != "ShooterLvl2"){
                SceneManager.LoadScene("ShooterLvl3");
                        exit = true;
            } else {
                SceneManager.LoadScene("ShooterLvl2");
                        exit = true;
            }
            
        }
    }

    void OnTriggerEnter(Collider col) {
        
        if(col.gameObject.name == "Enemy" || col.gameObject.name == "Enemy(Clone)") {
            speechOut.Speak("Ouch!");
            health -= enemyDamage;
        }
    }
}
