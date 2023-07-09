using DualPantoFramework;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class WallScript : MonoBehaviour
{
    private Equalizer Equalizer;
    private AudioFilterPeakingFilter filter;
    private GameObject[] vertWalls;
    private GameObject[] horiWalls;
    private int currentBand;
    private float currentGain;
    private GameObject itHandle;
    private GameObject meHandle;
    private PantoHandle meHandlePanto;
    private PantoHandle itHandlePanto;
    private float meHandleHeight;
    private float itHandleHeight;
    private int numBands;
    private float width;
    private bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        

        Equalizer = GameObject.Find("Equalizer").GetComponent<Equalizer>();
        filter = Equalizer.filter;
        vertWalls = GameObject.FindGameObjectsWithTag("VertWall");
        horiWalls = GameObject.FindGameObjectsWithTag("HoriWall");
        itHandle = GameObject.FindGameObjectWithTag("ItHandleObject");
        meHandle = GameObject.FindGameObjectWithTag("MeHandleObject");
        meHandlePanto = GameObject.Find("Panto").GetComponent<UpperHandle>();
        itHandlePanto = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandleHeight = meHandle.transform.position.y;
        itHandleHeight = itHandle.transform.position.y;
        currentBand = 0;
        currentGain = 0f;
        numBands = Equalizer.filter.dbGain.Length;
        width = 1f;
        horiWalls[0].GetComponent<PantoBoxCollider>().Disable();
        horiWalls[0].GetComponent<BoxCollider>().enabled = false;
        horiWalls[1].GetComponent<PantoBoxCollider>().Disable();
        horiWalls[1].GetComponent<BoxCollider>().enabled = false;
        vertWalls[0].GetComponent<PantoBoxCollider>().Disable();
        vertWalls[0].GetComponent<BoxCollider>().enabled = false;
        vertWalls[1].GetComponent<PantoBoxCollider>().Disable();
        vertWalls[1].GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    async void Update()
    {
        

        int i = getCurrentBand();
        float j = getCurrentGain();

        if (i != currentBand && ready)
        {
            currentBand = i;
            float gain = filter.dbGain[i];
            float newZ = Mathf.Lerp(-13f,-4f,(gain - Equalizer.minGain) / (Equalizer.maxGain - Equalizer.minGain));


            vertWalls[0].transform.position = new Vector3((float)i / numBands * 20f - 10f, meHandleHeight, -10);
            vertWalls[1].transform.position = new Vector3((float)i / numBands * 20f - 10f + width, meHandleHeight, -10);
            horiWalls[0].transform.position = new Vector3(0, itHandleHeight, newZ+width/2);
            horiWalls[1].transform.position = new Vector3(0, itHandleHeight, newZ-width/2);


            horiWalls[0].GetComponent<PantoBoxCollider>().Disable();
            horiWalls[0].GetComponent<BoxCollider>().enabled = false;
            horiWalls[1].GetComponent<PantoBoxCollider>().Disable();
            horiWalls[1].GetComponent<BoxCollider>().enabled = false;
            vertWalls[0].GetComponent<PantoBoxCollider>().Disable();
            vertWalls[0].GetComponent<BoxCollider>().enabled = false;
            vertWalls[1].GetComponent<PantoBoxCollider>().Disable();
            vertWalls[1].GetComponent<BoxCollider>().enabled = false;

            await meHandlePanto.MoveToPosition(new Vector3((float)i / numBands * 20f - 10f + width / 2, meHandleHeight, newZ));
            await itHandlePanto.MoveToPosition(new Vector3((float)i / numBands * 20f - 10f + width / 2, itHandleHeight, newZ));

            vertWalls[0].GetComponent<PantoBoxCollider>().Enable();
            vertWalls[0].GetComponent<BoxCollider>().enabled = true;
            vertWalls[1].GetComponent<PantoBoxCollider>().Enable();
            vertWalls[1].GetComponent<BoxCollider>().enabled = true;
            horiWalls[0].GetComponent<PantoBoxCollider>().Enable();
            horiWalls[0].GetComponent<BoxCollider>().enabled = true;
            horiWalls[1].GetComponent<PantoBoxCollider>().Enable();
            horiWalls[1].GetComponent<BoxCollider>().enabled = true;
        }
        if(j != currentGain && ready)
        {
            currentGain = j;
            Equalizer.updateGain();
            float newZ = Mathf.Lerp(-13f, -4f, (currentGain - Equalizer.minGain) / (Equalizer.maxGain - Equalizer.minGain));

            horiWalls[0].transform.position = new Vector3(0, itHandleHeight, newZ + width / 2);
            horiWalls[1].transform.position = new Vector3(0, itHandleHeight, newZ - width / 2);

            horiWalls[0].GetComponent<PantoBoxCollider>().Disable();
            horiWalls[0].GetComponent<BoxCollider>().enabled = false;
            horiWalls[1].GetComponent<PantoBoxCollider>().Disable();
            horiWalls[1].GetComponent<BoxCollider>().enabled = false;

            await itHandlePanto.MoveToPosition(new Vector3((float)i / numBands * 20f - 10f + width / 2, itHandleHeight, newZ));

            horiWalls[0].GetComponent<PantoBoxCollider>().Enable();
            horiWalls[0].GetComponent<BoxCollider>().enabled = true;
            horiWalls[1].GetComponent<PantoBoxCollider>().Enable();
            horiWalls[1].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Enable()
    {
        ready = true;
        vertWalls[0].GetComponent<PantoBoxCollider>().Enable();
        vertWalls[0].GetComponent<BoxCollider>().enabled = true;
        vertWalls[1].GetComponent<PantoBoxCollider>().Enable();
        vertWalls[1].GetComponent<BoxCollider>().enabled = true;
        horiWalls[0].GetComponent<PantoBoxCollider>().Enable();
        horiWalls[0].GetComponent<BoxCollider>().enabled = true;
        horiWalls[1].GetComponent<PantoBoxCollider>().Enable();
        horiWalls[1].GetComponent<BoxCollider>().enabled = true;
    }

    public int getCurrentBand()
    {
        return (int)(numBands * (itHandle.transform.position.x + 10f) / 20f);
    }
    public float getCurrentGain()
    {
        return Equalizer.zPosToGain(meHandle.transform.position.z);
    }
}
