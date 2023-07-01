using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class SpawnManager : MonoBehaviour
{
    public GameObject fruitPrefab;
    SpeechOut speechOut = new SpeechOut();

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
        GameObject fruit = Instantiate(fruitPrefab, new Vector3(0, 1f, -10), fruitPrefab.transform.rotation);
        await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(fruit);
    }
}
