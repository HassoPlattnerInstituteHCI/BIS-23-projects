using DualPantoFramework;
using UnityEngine;

public class MiniGolfV2 : MonoBehaviour
{
    private enum GameModes
    {
        ExploreMode, // Explore Mode: Explore the Map with the lower handle 
        ShootMode, // Shoot Mode: Shoot the golf ball using the lower handle by releasing it
        WatchMode // Watch Mode: The Ball has been shot. The player is watching the ball move until it stopped.
    }

    [Range(0, 2)] public float distanceThreshold = 0.5f; // Threshold for waiting position: Eliminate jitter
    [Range(0, 5)] public float waitTime = 1f; // Wait this time on GolfBall to change mode or to let go

    // When shooting the GolfBall, this is the multiplier of the Distance to calculate the force
    [Range(0, 5)] public float forceMultiplier = 1f;

    // Components of Panto
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    // Components of GolfBall
    private Rigidbody _rigidbody;

    // Shooting Mode Variables

    // Here we store the left-over time of
    // how long we need to stay on the GolfBall
    // to be registered to switch to shooting mode
    private float _golfBallWaitTime;
    private bool _isOnGolfBall; // The lower Handle is currently on the GolfBall
    private bool _letGo; // We are in shooting mode and have let go of the handle

    private Vector3 _letGoForce; // the force to be applied to handle and GolfBall upon release of the lower handle

    private Vector3 _letGoPosition = Vector3.zero; // The position of where the GolfBall is let got

    private GameModes _currentGameMode = GameModes.ExploreMode;

    // Start is called before the first frame update
    private async void Start()
    {
        // Initialize Panto Variables
        _upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();

        // Initialize GolfBall Components
        _rigidbody = GetComponent<Rigidbody>();

        // Move Handles
        _lowerHandle.StopApplyingForce();
        await _upperHandle.MoveToPosition(gameObject.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If we are in explore mode and ItHandle is on GolfBall:
        // Start waiting on GolfBall to switch game mode to ShootMode
        if (other.gameObject.name == "ItHandleGodObject" && _currentGameMode == GameModes.ExploreMode)
        {
            Debug.LogWarning("Start waiting on GolfBall");
            _isOnGolfBall = true;
            _golfBallWaitTime = waitTime;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // ItHandle still on GolfBall and currently waiting to switch game mode?
        // Reduce left wait time by passed time.
        // If time is over: Stop waiting on GolfBall and switch GameMode to ShootMode
        if (_isOnGolfBall && _currentGameMode == GameModes.ExploreMode && other.gameObject.name == "ItHandleGodObject")
        {
            _golfBallWaitTime -= Time.deltaTime;
            if (_golfBallWaitTime <= 0)
            {
                Debug.LogWarning("Switched to shoot mode");
                _currentGameMode = GameModes.ShootMode;
                _isOnGolfBall = false;
            }
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        // Stop waiting to switch game mode, if ItHandle left GolfBall
        if (_isOnGolfBall && _currentGameMode == GameModes.ExploreMode && other.gameObject.name == "ItHandleGodObject")
        {
            Debug.LogWarning("Stopped waiting on Golf Ball");
            _isOnGolfBall = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 pLowerHandle = _lowerHandle.GetPosition(); // Position of LowerHandle
        Vector3 pGolfBall = gameObject.transform.position; // Position of GolfBall
        float dLowerHandle = Vector3.Distance(pGolfBall, pLowerHandle); // Distance GolfBall <--> LowerHandle
        
        if (_currentGameMode == GameModes.ShootMode) // ShootMode Related Updates
        {
            if (_letGo) // We're in Shoot Mode and let go
            {
                if (_isOnGolfBall) // We touched the GolfBall with the LowerHandle
                {
                    // This is the point at which we shoot the ball in Unity
                    Debug.LogWarning("Shooting Ball with force: " + _letGoForce);
                    _rigidbody.AddForce(_letGoForce); // Move GolfBall in Unity
                    _currentGameMode = GameModes.WatchMode; // Now the player has to watch the Ball move
                    _upperHandle.SwitchTo(gameObject); // UpperHandle should now watch the ball
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
                // TODO IDEE: Nach dem loslassen einfach das lowerhandle weiter zum Ball bringen, egal was die impact-Force ist
                _lowerHandle.ApplyForce(dLowerHandle > distanceThreshold ? _letGoForce : Vector3.zero);
                
                // Distance to last letGoPosition 
                float dWaitPosition = Vector3.Distance(pLowerHandle, _letGoPosition);
                if (dWaitPosition > distanceThreshold) // If it has changed enough
                {
                    Debug.LogWarning("Start waiting for let go");
                    _letGoPosition = pLowerHandle; // Update Position
                    _golfBallWaitTime = waitTime; // Restart wait timer
                }
                else // If it has not changed much, we are still on the position
                {
                    _golfBallWaitTime -= Time.deltaTime; // Removed passed time from left time on timer
                    if (_golfBallWaitTime <= 0) // Waited Enough: We have let go now!
                    {
                        Debug.LogWarning("Let go of the Handle");
                        _letGo = true;
                    }
                }
            }
        }
    }
}