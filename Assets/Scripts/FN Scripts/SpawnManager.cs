using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    public GameObject fruitPrefab;
    public int force;
    public bool forceVarying = false;
    public bool random;
    public Vector3 spawnPosition = Vector3.zero;
    public bool curved;
    SpeechOut speechOut = new SpeechOut();
    private Vector3 curveDir = new Vector3(0, 0, 1);
    PantoHandle handle;

    public float introTime;
    private bool inIntro = true;
    static int level = 0;

    
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
        level++;
        print("level: " + level);
        if (level <= 3) 
            SceneManager.LoadScene(level);
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
        if (random) { spawnPosition = new Vector3(Random.Range(5, -5),0,-8); };
        spawnFruitBool = true;
        await handle.MoveToPosition(spawnPosition, 100);
        SpawnFruit("Erdbeere");

    }

    public void SpawnFruit (string type)
    {
        //Frucht mit Kurve und Force muss iwie immer in Panto Reichweite bleiben

        if (curved)
        {
            float randomFactor = Random.Range(10, 45);
            if (spawnPosition.x > 0)
                randomFactor *= -1;

            randomFactor = randomFactor * (Mathf.Abs(spawnPosition.x) / Mathf.Abs(5));

            curveDir = Quaternion.AngleAxis(/*randomFactor*/ 45, new Vector3(0, 1, 0)) * new Vector3(0, 0, 1);

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
        
        fruit.GetComponent<Rigidbody>().AddForce(curveDir * force, ForceMode.Impulse);
        spawnCounter++;
    }
}
