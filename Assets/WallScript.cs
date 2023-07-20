using DualPantoFramework;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

public class WallScript : MonoBehaviour
{
    private Equalizer Equalizer;
    private AudioFilterPeakingFilter filter;
    private GameObject[] vertWalls;
    private PantoCollider vertCollider1;
    private PantoCollider vertCollider2;
    private GameObject[] horiWalls;
    private PantoCollider horiCollider1;
    private PantoCollider horiCollider2;
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
        vertCollider1 = vertWalls[0].GetComponent<PantoBoxCollider>();
        vertCollider2 = vertWalls[1].GetComponent<PantoBoxCollider>();
        horiWalls = GameObject.FindGameObjectsWithTag("HoriWall");
        horiCollider1 = horiWalls[0].GetComponent<PantoBoxCollider>();
        horiCollider2 = horiWalls[1].GetComponent<PantoBoxCollider>();
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
        horiWalls[0].GetComponent<BoxCollider>().enabled = false;
        horiWalls[1].GetComponent<BoxCollider>().enabled = false;
        vertWalls[0].GetComponent<BoxCollider>().enabled = false;
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

            await MoveWallsAndHandles(new Vector3((float)i / numBands * 20f - 10f, meHandleHeight, -10), 
                new Vector3((float)i / numBands * 20f - 10f + width, meHandleHeight, -10),
                new Vector3(0, itHandleHeight, newZ + width / 2),
                new Vector3(0, itHandleHeight, newZ - width / 2),
                new Vector3((float)i / numBands * 20f - 10f + width / 2, meHandleHeight, newZ),
                new Vector3((float)i / numBands * 20f - 10f + width / 2, itHandleHeight, newZ)
                );
            /*
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
            */
        }
        if(j != currentGain && ready)
        {
            currentGain = j;
            Equalizer.updateGain();
            float newZ = Mathf.Lerp(-13f, -4f, (currentGain - Equalizer.minGain) / (Equalizer.maxGain - Equalizer.minGain));

            await MoveHoriWallsAndItHandle(new Vector3(0, itHandleHeight, newZ + width / 2),
                new Vector3(0, itHandleHeight, newZ - width / 2),
                new Vector3((float)i / numBands * 20f - 10f + width / 2, itHandleHeight, newZ));

            /*
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
            */
        }
    }

    public void Enable()
    {
        ready = true;

        vertCollider1.CreateObstacle();
        vertCollider1.Enable();
        vertWalls[0].GetComponent<BoxCollider>().enabled = true;
        vertCollider2.CreateObstacle();
        vertCollider2.Enable();
        vertWalls[1].GetComponent<BoxCollider>().enabled = true;
        horiCollider1.CreateObstacle();
        horiCollider1.Enable();
        horiWalls[0].GetComponent<BoxCollider>().enabled = true;
        horiCollider2.CreateObstacle();
        horiCollider2.Enable();
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

    private async Task MoveWallsAndHandles(Vector3 vert1,Vector3 vert2, Vector3 hori1, Vector3 hori2, Vector3 me, Vector3 it)
    {
        PantoCollider oldvert1Collider = vertCollider1;
        PantoCollider oldvert2Collider = vertCollider2;
        PantoCollider oldhori1Collider = horiCollider1;
        PantoCollider oldhori2Collider = horiCollider2;

        //clone obstacle to make sure we don't overwrite the reference to the old collider
        GameObject newVert1 = Instantiate(vertWalls[0]);
        GameObject newVert2 = Instantiate(vertWalls[1]);
        GameObject newHori1 = Instantiate(horiWalls[0]);
        GameObject newHori2 = Instantiate(horiWalls[1]);
        Destroy(vertWalls[0]);
        Destroy(vertWalls[1]);
        Destroy(horiWalls[0]);
        Destroy(horiWalls[1]);
        vertWalls[0] = newVert1;
        vertWalls[1] = newVert2;
        horiWalls[0] = newHori1;
        horiWalls[1] = newHori2;
        vertWalls[0].transform.position = vert1;
        vertWalls[1].transform.position = vert2;
        horiWalls[0].transform.position = hori1;
        horiWalls[1].transform.position = hori2;
        vertCollider1 = vertWalls[0].GetComponent<PantoCollider>();
        vertCollider2 = vertWalls[1].GetComponent<PantoCollider>();
        horiCollider1 = horiWalls[0].GetComponent<PantoCollider>();
        horiCollider2 = horiWalls[1].GetComponent<PantoCollider>();
        horiWalls[0].GetComponent<BoxCollider>().enabled = false;
        horiWalls[1].GetComponent<BoxCollider>().enabled = false;
        vertWalls[0].GetComponent<BoxCollider>().enabled = false;
        vertWalls[1].GetComponent<BoxCollider>().enabled = false;
        oldvert1Collider.Remove();
        oldvert2Collider.Remove();
        oldhori1Collider.Remove();
        oldhori2Collider.Remove();

        //move to
        await meHandlePanto.MoveToPosition(me,20);
        await itHandlePanto.MoveToPosition(it,20);

        vertCollider1.CreateObstacle();
        vertCollider2.CreateObstacle();
        horiCollider1.CreateObstacle();
        horiCollider2.CreateObstacle();
        vertCollider1.Enable();
        vertCollider2.Enable();
        horiCollider1.Enable();
        horiCollider2.Enable();
        horiWalls[0].GetComponent<BoxCollider>().enabled = true;
        horiWalls[1].GetComponent<BoxCollider>().enabled = true;
        vertWalls[0].GetComponent<BoxCollider>().enabled = true;
        vertWalls[1].GetComponent<BoxCollider>().enabled = true;
        //await Task.Delay(2000);
    }
    private async Task MoveHoriWallsAndItHandle(Vector3 hori1, Vector3 hori2, Vector3 it)
    {
        PantoCollider oldhori1Collider = horiCollider1;
        PantoCollider oldhori2Collider = horiCollider2;

        //clone obstacle to make sure we don't overwrite the reference to the old collider
        GameObject newHori1 = Instantiate(horiWalls[0]);
        GameObject newHori2 = Instantiate(horiWalls[1]);
        Destroy(horiWalls[0]);
        Destroy(horiWalls[1]);
        horiWalls[0] = newHori1;
        horiWalls[1] = newHori2;
        horiWalls[0].transform.position = hori1;
        horiWalls[1].transform.position = hori2;
        horiCollider1 = horiWalls[0].GetComponent<PantoCollider>();
        horiCollider2 = horiWalls[1].GetComponent<PantoCollider>();
        horiWalls[0].GetComponent<BoxCollider>().enabled = false;
        horiWalls[1].GetComponent<BoxCollider>().enabled = false;

        oldhori1Collider.Remove();
        oldhori2Collider.Remove();

        await itHandlePanto.MoveToPosition(it,20);

        horiCollider1.CreateObstacle();
        horiCollider2.CreateObstacle();

        horiCollider1.Enable();
        horiCollider2.Enable();
        horiWalls[0].GetComponent<BoxCollider>().enabled = true;
        horiWalls[1].GetComponent<BoxCollider>().enabled = true;

        //await Task.Delay(2000);
    }
}
