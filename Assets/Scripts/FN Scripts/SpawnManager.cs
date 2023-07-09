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
    public float xPosition = 0;
    public float spawnIntervall = 0;
    public int fruitsToWin = 1;
    public bool curved;
    SpeechOut speechOut = new SpeechOut();
    private Vector3 curveDir = new Vector3(0, 0, 1);
    private float timePassed = 0;
    bool spawning = false;
    static int level = 0;

    public List<GameObject> fruits = new List<GameObject>();
    public int slicedFruitsCount = 0;
    private int spawnCounter = 0;
    void Start()
    {

        startGame();

    }

    private void Update()
    {

        if (spawnIntervall != 0)
        {
            timePassed += Time.deltaTime;
            if (timePassed > spawnIntervall && spawning && spawnCounter < fruitsToWin)
            {
                SpawnFruit("Erdbeere");
                timePassed = 0;
            }
        }
    }

    public void Fail()
    {
        spawning = false;
        Task read = speechOut.Speak("Oh you missed the fruit, lets try that again");
        Invoke("startGame", 3);
    }

    public void Win()
    {
        Task read = speechOut.Speak("You did it! Hooray");
        spawning = false;
        level++;
        print("level: " + level);
        if (level <= 2) 
            SceneManager.LoadScene(level);
    }

    public void startGame()
    {
        fruits.Clear();
        slicedFruitsCount = 0;
        timePassed = 0;
        spawning = true;
        SpawnFruit("Erdbeere");
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(fruit);
    }

    private void SpawnFruit (string type)
    {
        //Frucht mit Kurve und Force muss iwie immer in Panto Reichweite bleiben

        if (random) { xPosition = Random.Range(5, -5); };
        if (curved)
        {
            float randomFactor = Random.Range(10, 45);
            if (xPosition > 0)
                randomFactor *= -1;

            randomFactor = randomFactor * (Mathf.Abs(xPosition) / Mathf.Abs(5));

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
                fruit = Instantiate(fruitPrefab, new Vector3(xPosition, 0, -8), fruitPrefab.transform.rotation);
                fruit.GetComponent<Rigidbody>().AddForce(curveDir * force, ForceMode.Impulse);
                break;
            case "Kokosnuss":
                fruit = Instantiate(fruitPrefab, new Vector3(xPosition, 0, -8), fruitPrefab.transform.rotation);
                fruit.GetComponent<Rigidbody>().AddForce(curveDir * force, ForceMode.Impulse);
                break;
        }
        fruits.Add(fruit);
        spawnCounter++;
    }
}
