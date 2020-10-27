using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    public GameObject coin;
    public Terrain terrain;
    public int maxCount = 20;
    private int currentCount = 0;
    private bool waiting = false;
    private float x;
    private float y=0.5f;
    private float z;
    private float min;
    private float max;


    private void Update()
    {
        if (!waiting&&currentCount < maxCount)
        {
            waiting = true;
            Spawn();
            
        }
    }

    public int GetCurrentCount()
    {
        return currentCount;
    }

    public void DecreaseCount()
    {
        if (currentCount > 0)
        {
            currentCount--;
        }
    }

    IEnumerator WaitBetweenSpawns()
    {
        yield return new WaitForSeconds(1);
        waiting = false;
    }

    private void Spawn()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        min = 0;
        max = 1000;
        x = Random.Range(min, min + max);

        min = 0;
        max = 1000;
        z = Random.Range(min, min + max);

        Vector3 position = new Vector3(x, y, z);
        Instantiate(coin, position,Quaternion.Euler(90,0,0));
        maxCount++;
        StartCoroutine(WaitBetweenSpawns());

    }
}
