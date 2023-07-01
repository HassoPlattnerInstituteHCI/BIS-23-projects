using UnityEngine;
using DualPantoFramework;

public class FallingStone : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;
    bool isswitched = false;
    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        InvokeRepeating("FallDown", 2.0f, 2f);
    }

    async void FixedUpdate()
    {
        Debug.Log(transform.position.x);
        Debug.Log(upperHandle.HandlePosition(transform.position).x);
        var distance = transform.position.x - upperHandle.HandlePosition(transform.position).x;
        Debug.Log(distance);

        if (Mathf.Abs(distance) > 0.5)
        {
            if (distance < 0 && transform.position.x < 2)
            {
                transform.position += new Vector3(1, 0, 0);
            }
            else if(transform.position.x > -2)
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
    }

    void FallDown()
    {
        transform.position -= new Vector3(0, 0, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                upperHandle.Freeze();
            }
            else
            {
                upperHandle.Free();
            }
            free = !free;
        }
    }
}
