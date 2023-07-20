using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followIt : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject itHandleObject;
    void Start()
    {
        itHandleObject = GameObject.FindGameObjectWithTag("ItHandleObject");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = itHandleObject.transform.position + new Vector3(0f, 2f, 0f);
    }
}
