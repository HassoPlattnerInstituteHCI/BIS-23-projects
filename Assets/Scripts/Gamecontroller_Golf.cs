using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System.Threading.Tasks;

public class Gamecontroller_Golf : MonoBehaviour
{   
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Hole;
    [SerializeField] private GameObject Wall_l;
    [SerializeField] private GameObject Wall_r;
    [SerializeField] private GameObject Wall_u;
    [SerializeField] private GameObject Wall_d;

    private PantoHandle itHandle;
    private PantoHandle meHandle;
    // Start is called before the first frame update
    void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        meHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        itHandle.SwitchTo(Hole);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
