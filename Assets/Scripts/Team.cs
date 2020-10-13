using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Team : MonoBehaviour
{

    [SerializeField]
    private GameObject slotPrefab;

    public GameObject[] slots;
    public int name;

    public List<Player> teamMembers = new List<Player>();
    public List<string> teamNames = new List<string>();
    public void SpawnSlots(int number)
    {
        slots = new GameObject[number];
        for (int i = 0; i < number; i++)
        {
            GameObject slot=Instantiate(slotPrefab, this.transform);
            
           slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-(i+1) * 80);
            slot.GetComponent<TeamSlot>().team = name;
        }

    }
}
