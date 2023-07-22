using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PacMan : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            //RefreshClosestCoinTEST();
        }
    }
}
