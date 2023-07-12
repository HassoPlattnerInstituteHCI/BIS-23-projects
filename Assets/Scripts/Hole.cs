using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    [SerializeField] private AudioSource win;
    // Start is called before the first frame update
    void Start()
    {
        win.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win(){
        win.Play();
    }
}
