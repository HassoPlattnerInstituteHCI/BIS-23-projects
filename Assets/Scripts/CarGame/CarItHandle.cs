using UnityEngine;
using DualPantoFramework;

namespace CarGame
{
    public class CarItHandle : MonoBehaviour
    {
        PantoHandle lowerHandle;
        bool free = true;

        void Start()
        {
            lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        }

        void FixedUpdate()
        {
            // Vector3 newposition = new Vector3(transform.position.x, 0f, transform.position.z);
            // transform.position = lowerHandle.HandlePosition(newposition);

            // transform.position = newposition;
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
}
