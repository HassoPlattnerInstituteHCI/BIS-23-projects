using System;
using System.Collections;
using DualPantoFramework;
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
    [Range(0, 1)] public float heartbeatWaitTime = 0.5f; // Regular Update Time for slow changes, normally 0.5 seconds (it effects also other values, example loseSpeed)
     
    // When shooting the GolfBall, this is the multiplier of the Distance to calculate the force
    [Range(0, 5)] public float forceMultiplier = 1f;
    [Range(1, 50)] public float shootPowerMultiplier = 25f;
    [Range(0, 1)] public float loseSpeedMultiplier = 0.99f; // Golfball loses speed after it has been played, can also be effected by hearbeatWaitTime
    public String nextScene;
    
    // Components of Panto
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    // Components of GolfBall
    private Rigidbody _rigidbody;
    private Collider _collider;

    // Sounds
    private SoundsMinigolf _sounds; // Class with all Sound Parts
    private bool _soundPlays;

    // Shooting Mode Variables

    // Here we store the left-over time of
    // how long we need to stay on the GolfBall
    // to be registered to switch to shooting mode
    private float _golfBallWaitTimer;
    private float _heartbeatTimer = 0f; // left-over time of heartbeat
    
    private bool _isOnGolfBall; // The lower Handle is currently on the GolfBall
    private bool _letGo; // We are in shooting mode and have let go of the handle

    private Vector3 _letGoForce; // the force to be applied to handle and GolfBall upon release of the lower handle
    private Vector3 _letGoPosition = Vector3.zero; // The position of where the GolfBall is let got
    private Vector3 _lastVelocity; // used to bounce off walls

    public static GameModes currentGameMode = GameModes.ExploreMode;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Panto Variables
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();

        // Initialize GolfBall Components
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        SetTriggerEnabled(true); // probably useless

        // Move Handles
        _lowerHandle.StopApplyingForce();
        _upperHandle.SwitchTo(gameObject);

        // Sounds
        _sounds = GetComponent<SoundsMinigolf>();
    }

    // release all Forces of the handles
    private void OnApplicationQuit()
    {
        _lowerHandle.Free();
        _lowerHandle.FreeRotation();
        _upperHandle.Free();
        _upperHandle.FreeRotation();
    }

    // probably useless
    private void SetTriggerEnabled(bool isEnabled) 
    {
        _collider.isTrigger = isEnabled;
        _rigidbody.useGravity = !isEnabled;
    }

    // load next Scene (nextScene has the name of the next Scene)
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        currentGameMode = GameModes.ExploreMode;
        SceneManager.LoadScene(nextScene);
    }
    
    // when the Golfball triggers another object
    private void OnTriggerEnter(Collider other)
    {
        // If we are in explore mode and ItHandle is on GolfBall:
        // Start waiting on GolfBall to switch game mode to ShootMode
        if (currentGameMode == GameModes.ExploreMode)
        {
            if (other.gameObject.name == _itHandleGodObject)
            {
                _isOnGolfBall = true;
                Debug.LogWarning("Start waiting on GolfBall - to switch to ShootMode");
                _golfBallWaitTimer = waitTime;
                _sounds.SePlayLoad();
            }
        }

        if (currentGameMode == GameModes.ShootMode)
        {
            if (other.gameObject.name == _itHandleGodObject)
            {
                _isOnGolfBall = true;
            }
        }

        if (currentGameMode == GameModes.WatchMode)
        {
            if (other.gameObject.CompareTag("Wall")) // when golfball collides with a wall
            {
                var wallVector = Vector3.forward; // calculate the normal vector of a wall (only two options horizontal or vertical)
                if (other.transform.rotation.eulerAngles.y is > 45 and < 135 or > 225 and < 315)
                {
                    wallVector = Vector3.left;
                }
                Debug.LogWarning("Collision on Wall - ball gets reflected" + wallVector);
                _rigidbody.velocity = Vector3.Reflect(_lastVelocity, wallVector);
            }

            if (other.gameObject.CompareTag("Goal")) // when golfball collides with the goal
            {
                _sounds.SeHitGoal();
                Debug.LogWarning("Collision on Goal - Load next Scene");
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
            _golfBallWaitTimer -= Time.deltaTime;
            if (_golfBallWaitTimer <= 0) // When time is over - switch to ShootMode
            {
                Debug.LogWarning("Switched to shoot mode");
                currentGameMode = GameModes.ShootMode;
                _sounds.SpeShootMode();
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
                _sounds.SeStop();
            }
        }
    }

    private void FixedUpdate()
    {
        // Update Heartbeat Timer for small changes
        _heartbeatTimer -= Time.deltaTime;
        if (_heartbeatTimer <= 0)
        {
            _upperHandle.SwitchTo(gameObject); // send the upper Handle to the ball
            if (currentGameMode == GameModes.WatchMode)
            {
                _rigidbody.velocity *= loseSpeedMultiplier; // reduce velocity to slow down the ball
            }
            _heartbeatTimer = heartbeatWaitTime; // reset HeartBeat Timer
        }
        _lastVelocity = _rigidbody.velocity; // update lastVelocity
        
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
                    _lowerHandle.Free();
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
                    _golfBallWaitTimer = waitTime; // Restart wait time
                    _sounds.SeStop();
                    _soundPlays = false;
                }
                else // If it has not changed much, we are still on the position
                {
                    _golfBallWaitTimer -= Time.deltaTime; // Removed passed time from left time on timer
                    if (_golfBallWaitTimer <= 0) // Waited Enough: We have let go now!
                    {
                        Debug.LogWarning("Let go of the Handle");
                        _letGo = true;
                        _lowerHandle.SwitchTo(gameObject);
                        _sounds.SeStop();
                        _sounds.SpeWatchMode();
                    }
                    else if (!_soundPlays && _golfBallWaitTimer <= waitTime * 0.8)
                    {
                        _sounds.SePlayLoad();
                        _soundPlays = true;
                    }
                }
            }
        }
    }
}