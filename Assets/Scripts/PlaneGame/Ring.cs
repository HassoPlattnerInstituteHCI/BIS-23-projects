using System;
using System.Threading;
using System.Threading.Tasks;

using DualPantoFramework;

using UnityEngine;


namespace PlaneGame
{
    public class Ring : MonoBehaviour
    {
        private PantoHandle itHandle;

        private async void Start()
        {
            itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
            Debug.LogError("RING CREATED");
            await itHandle.SwitchTo(gameObject);
            await Task.Delay(2000);
        }

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.025F);
            
            if (transform.position.z <= -18)
            {
                Destroy(gameObject);
                ScoreManager.misses++;
                ScoreManager.lives--;
            }
        }
    }
}