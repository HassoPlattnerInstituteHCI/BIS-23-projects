using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class SpawnManager : MonoBehaviour
{
    public GameObject fruitPrefab;
    public int Force;
    public bool random;
    SpeechOut speechOut = new SpeechOut();
    int xPosition=0;

    void Start()
    {
        
        
    }


    public void Fail()
    {
        Task read = speechOut.Speak("Oh you missed the fruit, lets try that again");
        Invoke("startGame", 3);
    }

    public void Win()
    {
        Task read = speechOut.Speak("You did it! Hooray");
    }

    public async void startGame()
    {
        if (random) { xPosition = Random.Range(5, -5); };

        GameObject fruit = Instantiate(fruitPrefab, new Vector3(xPosition, 0, -8), fruitPrefab.transform.rotation);
        fruit.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
        //await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(fruit);
    }
}
