using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SpeechIO;

using DualPantoFramework;
using UnityEngine;

namespace PlaneGame
{
    public class Plane : MonoBehaviour
    {
        public static PantoHandle MeHandle;
        private GameObject _ring;
        private float _rotation;
        private static Plane _plane;
        private AudioSource _audioSource;
        private Task _moveHandle;

        private SpeechIn speechIn;
        private SpeechOut speechOut;
        private SoundEffects soundEffects;

        // Start is called before the first frame update
        async void Start()
        {
            MeHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
            _plane = FindObjectOfType<Plane>();
            await MeHandle.MoveToPosition(_plane.gameObject.transform.position);
            MeHandle.Freeze();
            Debug.LogWarning("Moved.");
            MeHandle.FreeRotation();
            Debug.LogWarning("Freed rotation.");
            /*var delay = Task.Run(async () => { await Task.Delay(800); });
            delay.Wait();*/ // might be necessary later

            speechOut = new SpeechOut();
            soundEffects = FindObjectOfType<GameManager>().GetComponent<SoundEffects>();
        }

        // Update is called once per frame
        void Update()
        {
            if (MeHandle.inTransition)
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
                    _rotation = MeHandle.GetRotation();

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
                soundEffects.HitRing();
                Destroy(other.gameObject);
                ScoreManager.Score++;
                soundEffects.AnnounceScore(ScoreManager.Score);
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