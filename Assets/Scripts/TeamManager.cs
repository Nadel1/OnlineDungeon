using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] teams;

    [SerializeField]
    private GameObject players;


    [SerializeField]
    private GameObject playerPrefab;
    private List<Player> playerList = new List<Player>();
    private Dictionary<int, List<Player>> teamMembers = new Dictionary<int, List<Player>>();
    Player[] allPlayers;
    // Start is called before the first frame update
    void Awake()
    {
        //playerList = PhotonNetwork.CurrentRoom.Players.Values.ToList();
        //generate slots
        foreach(GameObject team in teams)
        {
            team.GetComponent<Team>().SpawnSlots(2);
        }


        allPlayers = PhotonNetwork.PlayerListOthers;
        playerList = PhotonNetwork.CurrentRoom.Players.Values.ToList();
        //generate players
        foreach (Player p in playerList)
        {
            GameObject label=Instantiate(playerPrefab, players.transform);
            label.GetComponentInChildren<Text>().text = p.NickName;
            label.GetComponent<PlayerRef>().player = p;
            label.GetComponent<PlayerRef>().name = p.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
