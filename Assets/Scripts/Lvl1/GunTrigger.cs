using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using UnityEngine.SceneManagement;


public class GunTrigger : MonoBehaviour
{

    private SpeechOut speechOut;
    // Start is called before the first frame update
    void Start()
    {
        speechOut = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter(Collider col) {

        if(col.gameObject.name == "Gun") {
            this.gameObject.tag = "armed";
            Destroy(col.gameObject);
            speechOut.Speak("You're armed");
            SceneManager.LoadScene("ShooterLvl2", LoadSceneMode.Single);
        
        }
        
    }
}