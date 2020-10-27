using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private GameObject coinsManager;

    private void Start()
    {
        coinsManager = GameObject.FindGameObjectWithTag("CoinsManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coinsManager.GetComponent<CoinsSpawner>().DecreaseCount();

            other.GetComponent<ScoreManager>().score += 100;
            Destroy(this.gameObject);
        }
    }
}
