using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 6f;
    public GameObject player;
    private Rigidbody enemyRb;
    
    private bool enemyActive = false;
    
    public string displayName;
    public AudioClip nameClip;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (transform.position.y < -2)
        {
            //GameObject.FindObjectOfType<SpawnManager>().FindOtherEnemy();
            Destroy(gameObject);
        }
    }
    
    // public async Task ActivateEnemy()
    // {
    //     enemyActive = true;
    // }


    void FixedUpdate()
    {
            /// challenge: set lookDirection to "enemy to player" vector
            Vector3 lookDirection = player.transform.position - transform.position;
            //transform.position += lookDirection.normalized * speed * Time.fixedDeltaTime;
           enemyRb.AddForce(lookDirection.normalized * speed * Time.fixedDeltaTime, ForceMode.Impulse);
     
    }

    void MoveToPlayer() {
        
    }
}
