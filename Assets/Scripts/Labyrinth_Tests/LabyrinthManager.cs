using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.SceneManagement;

public class LabyrinthManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public bool gameStarted = false;
    public bool playIntro = false;
    private SpeechOut speechOut;
    // private SpeechIn speech;
    private int numItems;
    private int itemCounter = 0;
    private UpperHandle upperHandle;
    private UpperHandle lowerHandle;

    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private async void Start() {
        PantoHandle lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        numItems = 1;
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = items[0];

        //await lowerHandle.MoveToPosition(item.transform.position, shouldFreeHandle: false);
        await lowerHandle.SwitchTo(item, 1f);
        //await StartGame();
        //await StartGame();
        //    if (!gameObject.GetComponent<LabyrinthSpawner>().enabled){
        //        numItems = 1;
        //        StartGame();
        //    }else{
        //        numItems = gameObject.GetComponent<LabyrinthSpawner>().numitems;
        //    }
    }

    public async Task StartGame() {
        speechOut = new SpeechOut();
        audioSource = GetComponent<AudioSource>();
        //if (playIntro)
        //{
        //    Level room = GameObject.Find("Panto").GetComponent<Level>();
        //    await room.PlayIntroduction();
        //}

        //await lowerHandle.SwitchTo(item);

        //getNextItem();
        
        // upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        // await upperHandle.MoveToPosition(spawn.transform.position, 10f, true);

        gameStarted = true;
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
        // speech.StopListening();
    }

    async public void getNextItem(){
        if(itemCounter >= numItems){
            SceneManager.LoadScene("LabyrinthCore");
        }else{
            itemCounter++;
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            GameObject item = items[0];

            //await lowerHandle.MoveToPosition(item.transform.position, shouldFreeHandle: false);
            await lowerHandle.SwitchTo(item);

            lowerHandle.Freeze();
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