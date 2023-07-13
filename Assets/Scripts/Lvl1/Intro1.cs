using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using DualPantoFramework;
using System.Threading.Tasks;

public class Intro1 : MonoBehaviour
{
    [SerializeField] public GameObject Spawn;
    Wall wall;


    private UpperHandle meHandle;
    // Start is called before the first frame update
    void Start()
    {
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>(); 
       meHandle.SwitchTo(Spawn);
       wall = gameObject.GetComponent<Wall>();
       Wait(1000);
       Wall.manualStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void Wait(int time){
        await Task.Delay(time);
        meHandle.Free();
    }
}
