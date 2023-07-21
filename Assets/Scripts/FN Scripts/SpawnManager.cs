using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    public GameObject fruitPrefab;
    public float force;
    public bool forceVarying = false;
    public bool random;
    public Vector3 spawnPosition = Vector3.zero;
    public bool curved;
    SpeechOut speechOut = new SpeechOut();
    private Vector3 curveDir = new Vector3(0, 0, 1);
    PantoHandle itHandle;

    public float introTime;
    private bool inIntro = true;
    
    public int fruitsToWin = 1;
    public int slicedFruitsCount = 0;

    public AudioClip success;
    public AudioClip fail;

    private int spawnCounter = 0;
    Task read;
    bool spawnFruitBool = false;
    float spawnDistTreshhold = 0.5f;
    GameObject audioManager;
    bool deletedGodObjects = false;
    public List<FruitType> typesOfFruitsToSpawn = new List<FruitType>();

    private void Awake()
    {
        EnableWalls();
    }

    async void Start()
    {
        FNIntroductionManager manager = GameObject.FindObjectOfType<FNIntroductionManager>();
        audioManager = GameObject.Find("AudioManager");
        itHandle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        await manager.PlayIntro();
        startGame();
        //Invoke("startGame", introTime);
    }

    private void EnableWalls()
    {
        PantoCollider[] pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }

    private void Update()
    {


    }

    public async void Fail(FruitType type)
    {
        //AudioSource.PlayClipAtPoint(fail, transform.position);
        audioManager.GetComponent<AudioManager>().playSound("missedFruit");

        //audioManager.GetComponent<AudioManager>().playSound("Success");
        if (type == FruitType.Bombe)
            await speechOut.Speak("You just hit a bombe. Try NOT to do that");
        else
            await speechOut.Speak("Oh you missed the fruit, lets try that again");

        startGame();
    }

    public void Win()
    {
        audioManager.GetComponent<AudioManager>().playSound("Success");

        //AudioSource.PlayClipAtPoint(success, transform.position);
        //read = speechOut.Speak("You did it! Hooray");

        int newSceneindex = int.Parse(SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1].ToString());
        if (newSceneindex < SceneManager.sceneCountInBuildSettings - 1)
            levelTransit(newSceneindex + 1);

        
    }

    public void startGame()
    {
        Debug.Log("Gamestart");
        spawnCounter = 0;
        slicedFruitsCount = 0;
        CalculateNewSpawnPosition();
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(fruit);
    }

    private void FixedUpdate()
    {
       //if (GameObject.Find("MeHandleGodObject") && GameObject.Find("ItHandleGodObject") && !deletedGodObjects)
       // {
       //     GameObject.Find("MeHandleGodObject").GetComponent<SphereCollider>().enabled = false;
       //     GameObject.Find("ItHandleGodObject").GetComponent<SphereCollider>().enabled = false;

       //     deletedGodObjects = true;
       // }
    }

    async public void CalculateNewSpawnPosition ()
    {
        if (random) { spawnPosition = new Vector3(Random.Range(4.5f, -4.5f), spawnPosition.y, spawnPosition.z); };
        spawnFruitBool = true;
        await itHandle.MoveToPosition(spawnPosition, 100);

        int randomFruit = 0;
        if (typesOfFruitsToSpawn.Count > 1)
        {
            
            if (typesOfFruitsToSpawn.Contains(FruitType.Bombe))
            {
                //Bomben haben 1/5 Chance zu spawnen
                bool spawnBomb = Random.Range(1,6) == 1 ? true : false;
                if (spawnBomb)
                    randomFruit = typesOfFruitsToSpawn.Count - 1;
                else
                    randomFruit = Random.Range(0, typesOfFruitsToSpawn.Count-1);

            }
            else
            {
                randomFruit = Random.Range(0, typesOfFruitsToSpawn.Count);

            }
        }

        SpawnFruit(typesOfFruitsToSpawn[randomFruit]);

    }

    public void SpawnFruit (FruitType type)
    {
        //Frucht mit Kurve und Force muss iwie immer in Panto Reichweite bleiben
        float arenaLength = 9f;
        float curveFactor = 0f;

        float minAngle = 0f;
        float maxAngle = 30f;
        if (curved)
        {
            // 50/50, ob links- oder rechts-Kurve
            bool left = (Random.Range(1, 3) == 1) ? false : true;
            
            if (left)
            {
                float interpolation = (Mathf.Abs(spawnPosition.x) + (arenaLength / 2)) / arenaLength;
                if (spawnPosition.x < 0)
                {
                    interpolation = 1 - interpolation;
                }

                // interpolates between 0 and 40, depending on the spawnposition in the arena
                curveFactor = -Mathf.Lerp(minAngle, maxAngle, interpolation);
            }
            else
            {
                float interpolation = (Mathf.Abs(spawnPosition.x) + (arenaLength / 2)) / arenaLength;
                if (spawnPosition.x > 0)
                {
                    interpolation = 1 - interpolation;
                }
                curveFactor = Mathf.Lerp(minAngle, maxAngle, interpolation);

            }
            print("CURVEFACTOR: " + curveFactor);

            curveDir = Quaternion.AngleAxis(curveFactor, new Vector3(0, 1, 0)) * new Vector3(0, 0, 1);

        }
        if (forceVarying)
        {
            force = Random.Range(force-2, force+2);
        }

        GameObject fruit = null;
        fruit = Instantiate(fruitPrefab, spawnPosition, fruitPrefab.transform.rotation);
        fruit.GetComponent<Fruit>().type = type;

        //Wenn Force straight nach oben geht, weniger applyen, da sonst außerhalb des Pantobereichs fliegt
        float forceDamp = Mathf.Lerp(0.6f, 1.0f, (maxAngle - minAngle) / (Mathf.Abs(curveFactor) - minAngle));
        force *= forceDamp;
        
        fruit.GetComponent<Rigidbody>().AddForce(curveDir.normalized * force, ForceMode.Impulse);
        spawnCounter++;
    }

    public async void levelTransit(int newSceneindex)
    {
        //await GameObject.Find("Panto").GetComponent<UpperHandle>().MoveToPosition(Vector3.zero, 1);
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().MoveToPosition(Vector3.zero, 1);
        SceneManager.LoadScene(newSceneindex);

    }
}



