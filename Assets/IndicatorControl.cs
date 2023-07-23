using DualPantoFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorControl : MonoBehaviour
{
    GameObject soundIndicator;
    Boolean controlEnabled = false;
    Boolean isInside = false;
    Boolean hasToInitiateMePosition = true;
    Boolean isManipulating = false;
    PantoHandle meHandle;
    PantoHandle itHandle;
    GameObject meHandleObject;
    GameObject itHandleObject;
    GameObject itFollower;
    Boolean isSwitched = false;
    // Start is called before the first frame update
    async void Start()
    {
        meHandleObject = GameObject.FindGameObjectWithTag("MeHandleObject");
        itHandleObject = GameObject.FindGameObjectWithTag("ItHandleObject");
        itFollower = GameObject.FindGameObjectWithTag("ItFollower");
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        soundIndicator = GameObject.Find("SoundIndicator");
        
    }

    // Update is called once per frame
    async void Update()
    {
        
        if (isInside && Input.GetKey("space") && controlEnabled)
        {
            //Debug.Log("Is inside");
            isManipulating = true;
            
        }
        else if(controlEnabled && !isManipulating)
        {
            itHandle.Free();
            if (!isSwitched)
            {
                //Debug.Log("Switching Me to It");
                await meHandle.SwitchTo(itHandleObject);
                isSwitched = true;
                hasToInitiateMePosition = true;
            }
        }
        if (Input.GetKey("space") && isManipulating)
        {
            if (hasToInitiateMePosition)
            {
                itHandle.Freeze();
                await meHandle.MoveToPosition(soundIndicator.transform.position + new Vector3(0, 0, 2.5f));
                hasToInitiateMePosition = false;
                meHandle.Free();
                isSwitched = false;
            }
            if (meHandleObject.transform.position.z < -8.5f && meHandleObject.transform.position.z > -15.5f)
            {
                soundIndicator.transform.position = new Vector3(soundIndicator.transform.position.x, soundIndicator.transform.position.y, meHandleObject.transform.position.z - 2.5f);
            }
            
            
        }
        else
        {
            isManipulating = false;
        }
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("ItHandleObject"))
        {
            isInside = true;
        }
    }//END FUNCTION ONCOLLISIONENTER

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ItHandleObject"))
        {
            isInside = false;
        }
    }

    async public void enableControl()
    {
        controlEnabled = true;
        await meHandle.SwitchTo(itHandleObject);
        Debug.Log("Switched to It Handle");
        isSwitched = true;
        hasToInitiateMePosition = true;
        isManipulating = false;
    }

}