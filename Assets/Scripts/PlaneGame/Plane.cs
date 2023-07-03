using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using DualPantoFramework;
using UnityEngine;

namespace PlaneGame
{
    public class Plane : MonoBehaviour
    {
        private static PantoHandle _meHandle;
        private GameObject _ring;
        private float _rotation;
        private static Plane _plane;
        private AudioSource _audioSource;
        private Task _moveHandle;

        // Start is called before the first frame update
        async void Start()
        {
            _meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
            _plane = FindObjectOfType<Plane>();
            _moveHandle  = _meHandle.SwitchTo(_plane.gameObject);
            await _moveHandle;
            Debug.LogWarning("Moved.");
            _meHandle.FreeRotation();
            Debug.LogWarning("Freed rotation.");
            /*var delay = Task.Run(async () => { await Task.Delay(800); });
            delay.Wait();*/ // might be necessary later

        }

        // Update is called once per frame
        void Update()
        {
            if (_meHandle.inTransition)
            {
                Debug.LogWarning("Handle not ready, standing by...");
                var delay = Task.Run(async () => { await Task.Delay(10); });
                delay.Wait();
            }
            else
            {
                try
                {
                    _ring = GameObject.FindGameObjectsWithTag("ring")[0];
                    _rotation = _meHandle.GetRotation();

                    _ring.transform.position = new Vector3(_ring.transform.position.x + MapAngleToForce(_rotation), 0,
                        _ring.transform.position.z);
                }
                catch (Exception)
                {
                    Debug.Log("Plane standing by, no rings present.");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ring"))
            {
                Destroy(other.gameObject);
                ScoreManager.Score++;
            }
        }

        static float MapAngleToForce(float angle)
        {
            float force = 0;
            bool neg = false;

            if (angle is <= 90 or >= 270)
            {
                if (angle >= 270)
                {
                    angle = 360 - angle;
                    neg = true;
                }

                force = 1F / 90F * angle;

            }
            else
            {
                neg = angle is >= 180 and < 270;
                force = 1;
            }

            return neg ? 0.035F * force : 0.035F * -force;
        }
    }
}