using UnityEngine;
using DualPantoFramework;

public class FallingStone : MonoBehaviour
{
    public GameObject copyPrefab;
    bool free = true;
    PantoHandle upperHandle;
    bool isswitched = false;
    int[] heightmap = {3, 1, 0, 2, 4};
    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        InvokeRepeating("FallDown", 2.0f, 0.5f);
        //await upperHandle.SwitchTo(gameObject, 20f);
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
        // if(Mathf.Abs(distance) > 0.2){
        //     upperHandle.MoveToPosition(transform.position, 100f, true);
        // }
    }

    void FallDown()
    {
        int xPos = (int) transform.position.x + 2;
        if(transform.position.z > (-14 + heightmap[xPos])){
            transform.position -= new Vector3(0, 0, 1);
        }else{
            GameObject fixedCube = Instantiate(copyPrefab, transform.position, copyPrefab.transform.rotation);
            PantoBoxCollider collider = fixedCube.AddComponent<PantoBoxCollider>();
            collider.CreateObstacle();
            collider.Enable();
            transform.position = new Vector3(0, 0, -3);
            heightmap[xPos]++;
        }
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
