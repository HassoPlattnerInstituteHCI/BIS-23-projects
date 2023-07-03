using System;
using DualPantoFramework;
using UnityEngine;

namespace PlaneGame
{
    public class Plane : MonoBehaviour
    {
        private PantoHandle _meHandle;
        private GameObject _ring;
        private float _rotation;
        private Plane _plane; // ? what exactly did I want to do with that?
        private AudioSource _audioSource;

        // Start is called before the first frame update
        async void Start()
        {
            _meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
            _plane = FindObjectOfType<Plane>();
            /*await _meHandle.SwitchTo(_plane.gameObject);
            Debug.LogWarning("Moved.");
            _meHandle.Freeze();
            Debug.LogWarning("Froze.");
            _meHandle.FreeRotation();
            Debug.LogWarning("Freed _rotation.");*/
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                _ring = GameObject.FindGameObjectsWithTag("_ring")[0];
                _rotation = _meHandle.GetRotation();

                _ring.transform.position = new Vector3(_ring.transform.position.x + MapAngleToForce(_rotation), 0,
                    _ring.transform.position.z);
            }
            catch (Exception)
            {
                Debug.Log("Plane standing by, no rings present.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "_ring")
            {
                Destroy(other.gameObject);
                ScoreManager.Score++;
            }
        }

        static float MapAngleToForce(float angle)
        {
            float force = 0;
            bool neg = false;

            if (angle is < 90 or > 270)
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
                if (angle is < 270 and > 180)
                {
                    angle = 360 - angle;
                    neg = true;
                }

                force = 1F / 180F * angle;
            }

            return neg ? 0.04F * force : 0.04F * -force;
        }
    }
}