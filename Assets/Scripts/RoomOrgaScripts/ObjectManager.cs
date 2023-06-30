using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class ObjectManager : MonoBehaviour
{
    public GameObject table;
    public GameObject selector;
    public bool gameStarted = false;
    public bool playIntro = false;
    private SpeechOut speechOut;
    private SpeechIn speech;

    async void Start()
    {
        StartGame();
        speech = new SpeechIn(onSpeechRecognized);
        speech.StartListening(new string[]{"pick up", "place"});
    }

    private void Update() {
        if(selector.activeSelf){
            FindOtherObject();
        }
    }

    async void StartGame() {
        if (playIntro) 
        {
            Level room = GameObject.Find("Panto").GetComponent<Level>();
            await room.PlayIntroduction();
        }
        gameStarted = true;
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        speech.StopListening();
    }

    public GameObject GetClosestGameObject(string tag, Vector3 position) 
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in gos) {
            float currentDistance = Vector3.Distance(go.transform.position, position);
     
            if (currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }
        return closest;
    }
    async public void FindOtherObject()
    {
        Vector3 selectorPosition = selector.transform.position;

        GameObject closestObject = GetClosestGameObject("Object", selectorPosition);
        if (closestObject != null)
            await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(closestObject);
    }

    async void onSpeechRecognized(string command) {
        if (command == "pick up" && selector.activeSelf) {
            selector.GetComponent<ObjectController>().pickup = true;
        } else if (command == "place" && !selector.activeSelf) {
            selector.SetActive(true);
            table.GetComponent<MeHandle>().enabled = false;
        }
    }

}
