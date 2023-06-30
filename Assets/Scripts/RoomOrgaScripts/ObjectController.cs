using UnityEngine;
using DualPantoFramework;
using SpeechIO;
using System.Threading.Tasks;

public class ObjectController : MonoBehaviour
{
    public bool pickup = false;
    private void OnCollisionEnter(Collision other) {
        if(!pickup){ return; }
        if(other.gameObject.CompareTag("Object")){
            gameObject.SetActive(false);
            other.gameObject.GetComponent<MeHandle>().enabled = true;
            GameObject.Find("Panto").GetComponent<LowerHandle>().SwitchTo(GameObject.Find("RoomLayout"));
            pickup = false;
        }
    }
}
