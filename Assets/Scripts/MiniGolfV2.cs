using System;
using System.Collections;
using DualPantoFramework;
using SpeechIO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MiniGolfV2 : MonoBehaviour
{
    private readonly String _itHandleGodObject = "ItHandleGodObject";

    public enum GameModes
    {
        ExploreMode, // Explore Mode: Explore the Map with the lower handle 
        ShootMode, // Shoot Mode: Shoot the golf ball using the lower handle by releasing it
        WatchMode // Watch Mode: The Ball has been shot. The player is watching the ball move until it stopped.
    }

    [Range(0, 2)] public float distanceThreshold = 0.5f; // Threshold for waiting position: Eliminate jitter
    [Range(0, 5)] public float waitTime = 1f; // Wait this time on GolfBall to change mode or to let go

    // When shooting the GolfBall, this is the multiplier of the Distance to calculate the force
    [Range(0, 5)] public float forceMultiplier = 1f;
    [Range(1, 50)] public float shootPowerMultiplier = 25f;
    public AudioClip clipLoad;
    public AudioClip clipSuccess;
    public String nextScene;
    
    // Components of Panto
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    // Components of GolfBall
    private Rigidbody _rigidbody;
    private Collider _collider;
    private AudioSource _audioSource;
    
    // Other Components
    public static SpeechOut speechOut;
    
    private bool _soundPlays;

    // Shooting Mode Variables

    // Here we store the left-over time of
    // how long we need to stay on the GolfBall
    // to be registered to switch to shooting mode
    private float _golfBallWaitTime;
    private bool _isOnGolfBall; // The lower Handle is currently on the GolfBall
    private bool _letGo; // We are in shooting mode and have let go of the handle

    private Vector3 _letGoForce; // the force to be applied to handle and GolfBall upon release of the lower handle

    private Vector3 _letGoPosition = Vector3.zero; // The position of where the GolfBall is let got
    
    public static GameModes currentGameMode = GameModes.ExploreMode;

    // Start is called before the first frame update
    private void Start()
    {
        speechOut = new SpeechOut();
        
        // Initialize Panto Variables
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();

        // Initialize GolfBall Components
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();

        SetTriggerEnabled(true);

        // Move Handles
        _lowerHandle.StopApplyingForce();
        _upperHandle.SwitchTo(gameObject);
    }

    private void OnApplicationQuit()
    {
        _lowerHandle.Free();
        _upperHandle.Free();
    }

    private void SetTriggerEnabled(bool isEnabled)
    {
        _collider.isTrigger = isEnabled;
        _rigidbody.useGravity = !isEnabled;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nextScene);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // If we are in explore mode and ItHandle is on GolfBall:
        // Start waiting on GolfBall to switch game mode to ShootMode
        if (other.gameObject.name == _itHandleGodObject)
        {
            _isOnGolfBall = true;
            if (currentGameMode == GameModes.ExploreMode)
            {
                Debug.LogWarning("Start waiting on GolfBall");
                _golfBallWaitTime = waitTime;
                _audioSource.clip = clipLoad;
                _audioSource.Play(0);   
            }
        }

        if (currentGameMode == GameModes.WatchMode)
        {
            if (other.gameObject.CompareTag("Goal"))
            {
                _audioSource.clip = clipSuccess;
                _audioSource.Play();
                Debug.LogWarning("MEINE GÜTE GEHT DER AB");
                _rigidbody.velocity = Vector3.zero;
                _lowerHandle.Free();
                StartCoroutine(NextScene());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // ItHandle still on GolfBall and currently waiting to switch game mode?
        // Reduce left wait time by passed time.
        // If time is over: Stop waiting on GolfBall and switch GameMode to ShootMode
        if (_isOnGolfBall && currentGameMode == GameModes.ExploreMode && other.gameObject.name == _itHandleGodObject)
        {
            _golfBallWaitTime -= Time.deltaTime;
            if (_golfBallWaitTime <= 0)
            {
                Debug.LogWarning("Switched to shoot mode");
                currentGameMode = GameModes.ShootMode;
                _isOnGolfBall = false;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Stop waiting to switch game mode, if ItHandle left GolfBall
        if (_isOnGolfBall && other.gameObject.name == _itHandleGodObject)
        {
            _isOnGolfBall = false;
            if (currentGameMode == GameModes.ExploreMode)
            {
                Debug.LogWarning("Stopped waiting on Golf Ball");
                _audioSource.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 pLowerHandle = _lowerHandle.GetPosition(); // Position of LowerHandle
        Vector3 pGolfBall = gameObject.transform.position; // Position of GolfBall
        float dLowerHandle = Vector3.Distance(pGolfBall, pLowerHandle); // Distance GolfBall <--> LowerHandle
        
        if (currentGameMode == GameModes.ShootMode) // ShootMode Related Updates
        {
            if (_letGo) // We're in Shoot Mode and let go
            {
                if (_isOnGolfBall) // We touched the GolfBall with the LowerHandle
                {
                    // This is the point at which we shoot the ball in Unity
                    Debug.LogWarning("Shooting Ball with force: " + _letGoForce);
                    Vector3 moveForce = _letGoForce * shootPowerMultiplier;
                    moveForce.y = 0;
                    _rigidbody.velocity = moveForce; // Move GolfBall in Unity
                    currentGameMode = GameModes.WatchMode; // Now the player has to watch the Ball move
                    _upperHandle.SwitchTo(gameObject); // UpperHandle should now watch the ball
                    _lowerHandle.FreeRotation();
                    _lowerHandle.Freeze();
                    _letGo = false; // Not let go anymore for the next ShootMode
                }
            }
            else // We're in ShootMode and have not let go
            {
                // Update the Force on impact by first calculating the direction of the hit
                // then multiplying it with the negative of the distance to the lower handle 
                // and the forceMultiplier
                Vector3 direction = (pLowerHandle - pGolfBall).normalized;
                _letGoForce = direction * -(dLowerHandle * forceMultiplier);

                // Force Feedback on the lower handle towards the ball, if we are on the Ball do not exert any force
                _lowerHandle.ApplyForce(dLowerHandle > distanceThreshold ? _letGoForce : Vector3.zero);
                
                // Distance to last letGoPosition 
                float dWaitPosition = Vector3.Distance(pLowerHandle, _letGoPosition);
                if (dWaitPosition > distanceThreshold) // If it has changed enough
                {
                    Debug.LogWarning("Start waiting for let go");
                    _letGoPosition = pLowerHandle; // Update Position
                    _golfBallWaitTime = waitTime; // Restart wait time
                    _audioSource.Stop();
                    _soundPlays = false;
                }
                else // If it has not changed much, we are still on the position
                {
                    _golfBallWaitTime -= Time.deltaTime; // Removed passed time from left time on timer
                    if (_golfBallWaitTime <= 0) // Waited Enough: We have let go now!
                    {
                        Debug.LogWarning("Let go of the Handle");
                        _letGo = true;
                        _lowerHandle.SwitchTo(gameObject);
                        _audioSource.Stop();
                    }
                    else if (!_soundPlays && _golfBallWaitTime <= waitTime * 0.8)
                    {
                        _audioSource.clip = clipLoad;
                        _audioSource.Play();
                        _soundPlays = true;
                    }
                }
            }
        }
    }
}