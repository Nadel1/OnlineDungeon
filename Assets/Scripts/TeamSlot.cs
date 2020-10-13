using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;


public class TeamSlot : MonoBehaviour, IDropHandler
{
    public GameObject player;

    private List<Player> players = new List<Player>();

    Player p;
    public int team;

    public void SetPlayer(GameObject player)
    {
        
       /* if (this.player != null)
        {
            GetComponentInParent<Team>().teamMembers.Remove(player);
            p = player;
        }*/
        this.player = player;
        if (player != null&&player.GetComponent<PlayerRef>()!=null)
        {
            GetComponentInParent<Team>().teamNames.Add(player.GetComponent<PlayerRef>().name);
        }
        //GetComponentInParent<Team>().teamMembers.Add(player);
        //players = GetComponentInParent<Team>().teamMembers;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
