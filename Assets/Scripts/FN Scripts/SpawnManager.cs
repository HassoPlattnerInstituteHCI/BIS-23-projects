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
    PantoHandle handle;

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
    void Start()
    {
        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        Invoke("startGame", introTime);
        


    }


    private void Update()
    {


    }

    public void Fail()
    {
        AudioSource.PlayClipAtPoint(fail, transform.position);
        read = speechOut.Speak("Oh you missed the fruit, lets try that again");
        Invoke("startGame", 3);
    }

    public void Win()
    {
        AudioSource.PlayClipAtPoint(success, transform.position);
        read = speechOut.Speak("You did it! Hooray");

        int newSceneindex = int.Parse(SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1].ToString());
        if (newSceneindex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(newSceneindex);

        
    }

    public void startGame()
    {
        spawnCounter = 0;
        slicedFruitsCount = 0;
        CalculateNewSpawnPosition();
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(fruit);
    }

    private void FixedUpdate()
    {
       
    }

    async public void CalculateNewSpawnPosition ()
    {
        if (random) { spawnPosition = new Vector3(Random.Range(4.5f, -4.5f), spawnPosition.y, spawnPosition.z); };
        spawnFruitBool = true;
        await handle.MoveToPosition(spawnPosition, 100);
        SpawnFruit("Erdbeere");

    }

    public void SpawnFruit (string type)
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

                // interpolates between 0� and 40�, depending on the spawnposition in the arena
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
        switch (type)
        {
            case "Erdbeere":
                fruit = Instantiate(fruitPrefab, spawnPosition, fruitPrefab.transform.rotation);
                break;
            case "Kokosnuss":
                fruit = Instantiate(fruitPrefab, spawnPosition, fruitPrefab.transform.rotation);
                break;
        }

        //Wenn Force straight nach oben geht, weniger applyen, da sonst au�erhalb des Pantobereichs fliegt
        float forceDamp = Mathf.Lerp(0.6f, 1.0f, (maxAngle - minAngle) / (Mathf.Abs(curveFactor) - minAngle));
        force *= forceDamp;
        
        fruit.GetComponent<Rigidbody>().AddForce(curveDir.normalized * force, ForceMode.Impulse);
        spawnCounter++;
    }
}
