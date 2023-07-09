using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class VertWallScript : MonoBehaviour
{
    private Equalizer Equalizer;
    private GameObject[] vertWalls;
    private float currentBand;
    private GameObject itHandle;
    private GameObject meHandle;
    private PantoHandle meHandlePanto;
    private float meHandleHeight;
    private int numBands;
    private float width;
    private bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        

        Equalizer = GameObject.Find("Equalizer").GetComponent<Equalizer>();
        vertWalls = GameObject.FindGameObjectsWithTag("VertWall");
        itHandle = GameObject.FindGameObjectWithTag("ItHandleObject");
        meHandle = GameObject.FindGameObjectWithTag("MeHandleObject");
        meHandlePanto = GameObject.Find("Panto").GetComponent<UpperHandle>();
        meHandleHeight = meHandle.transform.position.y;
        currentBand = 0f;
        numBands = Equalizer.filter.dbGain.Length;
        width = 1f;
        vertWalls[0].GetComponent<PantoBoxCollider>().Disable();
        vertWalls[0].GetComponent<BoxCollider>().enabled = false;
        vertWalls[1].GetComponent<PantoBoxCollider>().Disable();
        vertWalls[1].GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    async void Update()
    {
        Vector3 position = itHandle.transform.position;
        
        int i = (int)(numBands * (position.x + 10f) / 20f);
        if (i != currentBand && ready)
        {
            currentBand = i;
            vertWalls[0].transform.position = new Vector3((float)i / numBands * 20f - 10f, meHandleHeight, -10);
            vertWalls[1].transform.position = new Vector3((float)i / numBands * 20f - 10f + width, meHandleHeight, -10);
            
            vertWalls[0].GetComponent<PantoBoxCollider>().Disable();
            vertWalls[0].GetComponent<BoxCollider>().enabled = false;
            vertWalls[1].GetComponent<PantoBoxCollider>().Disable();
            vertWalls[1].GetComponent<BoxCollider>().enabled = false;

            await meHandlePanto.MoveToPosition(new Vector3((float)i / numBands * 20f - 10f + width / 2, meHandleHeight, meHandle.transform.position.z));

            vertWalls[0].GetComponent<PantoBoxCollider>().Enable();
            vertWalls[0].GetComponent<BoxCollider>().enabled = true;
            vertWalls[1].GetComponent<PantoBoxCollider>().Enable();
            vertWalls[1].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Enable()
    {
        ready = true;
        vertWalls[0].GetComponent<PantoBoxCollider>().Enable();
        vertWalls[0].GetComponent<BoxCollider>().enabled = true;
        vertWalls[1].GetComponent<PantoBoxCollider>().Enable();
        vertWalls[1].GetComponent<BoxCollider>().enabled = true;
    }

    public int getCurrentBand()
    {
        return (int)(numBands * (itHandle.transform.position.x + 10f) / 20f);
    }
}
