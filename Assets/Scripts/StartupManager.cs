using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartupManager : MonoBehaviour
{
    GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().playSound("Intro1");
        StartCoroutine(waiter());
        //SceneManager.LoadScene("FruitNinja 1");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator waiter()
    {
        

        //Wait for 2 seconds
        yield return new WaitForSeconds(8);
        audioManager.GetComponent<AudioManager>().playSound("Track1");
        SceneManager.LoadScene("FruitNinja 1");


    }
}
