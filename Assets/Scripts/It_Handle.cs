////Default
//using UnityEngine;
//using DualPantoFramework;

//public class It_handle : MonoBehaviour
//{
//    PantoHandle lowerHandle;
//    bool free = true;
//    void Start()
//    {
//        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
//    }

//    void FixedUpdate()
//    {
//        transform.position = lowerHandle.HandlePosition(transform.position);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (free)
//            {
//                lowerHandle.Freeze();
//            }
//            else
//            {
//                lowerHandle.Free();
//            }
//            free = !free;
//        }
//    }
//}

//Pfeiltasten
using UnityEngine;
using DualPantoFramework;

public class It_handle : MonoBehaviour
{
    public float moveSpeed = 5f;

    PantoHandle lowerHandle;
    bool free = true;

    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    }

    void FixedUpdate()
    {
        // Bewegung des "ItHandle" basierend auf den Pfeiltasten-Eingaben
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Aktualisierung der Position des "ItHandle" durch den PantoHandle
        transform.position = lowerHandle.HandlePosition(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                lowerHandle.Freeze();
            }
            else
            {
                lowerHandle.Free();
            }
            free = !free;
        }
    }
}