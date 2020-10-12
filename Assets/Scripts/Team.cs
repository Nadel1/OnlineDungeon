using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    [SerializeField]
    private GameObject slotPrefab;

    public GameObject[] slots;
    public void SpawnSlots(int number)
    {
        slots = new GameObject[number];
        for (int i = 0; i < number; i++)
        {
            GameObject slot=Instantiate(slotPrefab, this.transform);
            
           slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-(i+1) * 80);
        }

    }
}
