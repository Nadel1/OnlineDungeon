using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGameManager : MonoBehaviour
{
    public float team1Score=0;
    public float team2Score=0;

    private List<ScoreManager> team1 = new List<ScoreManager>();
    private List<ScoreManager> team2 = new List<ScoreManager>();

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>().team == 0)
            {
                team1.Add(players[i].GetComponent<ScoreManager>());
            }
            else
            {
                team2.Add(players[i].GetComponent<ScoreManager>());
            }
        }

    }

    public void FinalizeScore()
    {
        foreach(ScoreManager score in team1)
        {
            team1Score += score.score;
        }
        foreach (ScoreManager score in team1)
        {
            team2Score += score.score;
        }
    }
}
