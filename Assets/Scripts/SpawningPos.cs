using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPos : MonoBehaviour
{
    public GameObject[] spawnPos;
    private int count = 0;

    public Transform nextSpawn()
    {
        int temp = count;
        count++;
        return spawnPos[temp].transform;

    }
}
