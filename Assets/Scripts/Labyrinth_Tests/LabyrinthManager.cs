using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;
using System.Collections;

public class LabyrinthManager : MonoBehaviour
{
    public GameObject player;
    public bool gameStarted = false;
    public bool playIntro = false;
    private SpeechOut speechOut;
    // private SpeechIn speech;
    public int numItems;
    private int itemCounter = 0;

    private AudioSource audioSource;
    public AudioClip[] audioClips;

    async void Start()
    {
        StartGame();
    }

    async void StartGame() {
        speechOut = new SpeechOut();
        audioSource = GetComponent<AudioSource>();
        if (playIntro) 
        {
            Level room = GameObject.Find("Panto").GetComponent<Level>();
            await room.PlayIntroduction();
        }
        gameStarted = true;
        getNextItem();
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        // speech.StopListening();
    }

    async public void getNextItem(){
        if(itemCounter >= numItems){
            // Next Level
        }else{
            itemCounter++;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Item");
            GameObject item = gos[Random.Range(0, gos.Length)];
            StartCoroutine(playNextItemSoundCoroutine(item));
            await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(item);
            item.GetComponent<Item>().currentItem = true;
        }
    }

    async public void playItemSound(GameObject item){
       StartCoroutine(playItemSoundCoroutine(item));
    }

    private IEnumerator playItemSoundCoroutine(GameObject item){
        AudioClip a = item.GetComponent<Item>().nameClip;
        audioSource.PlayOneShot(audioClips[0]);
        yield return new WaitForSeconds(audioClips[0].length);
        audioSource.PlayOneShot(a);
    }

    private IEnumerator playNextItemSoundCoroutine(GameObject item){
        AudioClip a = item.GetComponent<Item>().nameClip;
        audioSource.PlayOneShot(audioClips[1]);
        yield return new WaitForSeconds(audioClips[1].length);
        audioSource.PlayOneShot(a);
    }

    // public GameObject GetClosestGameObject(string tag, Vector3 position) 
    // {
    //     GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

    //     GameObject closest = null;
    //     float distance = Mathf.Infinity;

    //     foreach (GameObject go in gos) {
    //         float currentDistance = Vector3.Distance(go.transform.position, position);
     
    //         if (currentDistance < distance)
    //         {
    //             closest = go;
    //             distance = currentDistance;
    //         }
    //     }
    //     return closest;
    // }
    // async public void FindOtherObject()
    // {
    //     Vector3 selectorPosition = player.transform.position;

    //     GameObject closestObject = GetClosestGameObject("Item", selectorPosition);
    //     if (closestObject != null)
    //         await GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(closestObject);
    // }
}