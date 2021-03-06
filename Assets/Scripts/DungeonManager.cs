﻿/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

namespace Photon.Pun.Demo.PunBasics
{
    public class DungeonManager : MonoBehaviourPunCallbacks
    {
        public GameObject winnerUI;

        public GameObject team1Spawn;
        public GameObject team2Spawn;



        private GameObject player1;
        private GameObject player2;

        private GameObject DDOL;

        private Dictionary<int, List<Player>> teamMembers = new Dictionary<int, List<Player>>();

        private List<Player> team0 = new List<Player>();
        private List<Player> team1 = new List<Player>();
        private Launcher ls;
        private PhotonView PV;

        public int team=100;
        private bool setPlayer = false;
        // Start Method
        private void Start()
        {
            DDOL = GameObject.FindGameObjectWithTag("DDOL");
            PV = GetComponent<PhotonView>();
            if (PV.IsMine)
            {
                PV.RPC("RPC_GetTeam", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer);
            }
            player1=PhotonNetwork.Instantiate("Player", new Vector3(0,0,0), Quaternion.identity, 0);
            player1.GetComponentInChildren<PlayerMovement>().team = team;

        }
        // Update Method
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            GameObject[] spawnPos;
            Transform spawnAt = null;


        }

        // Photon Methods
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("OnPlayerLeftRoom() " + otherPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Lobby");
            }
        }
        //Helper Methods
        [PunRPC]
        void RPC_GetTeam(Player p)
        {
            teamMembers = DDOL.GetComponent<Launcher>().GetTeamMembers();
            team0 = teamMembers[0];
            //team1 = teamMembers[teamMembers.Count-1];
            team = team0.Contains(p) ? 0 : 1;
            PV.RPC("RPC_SendTeam", RpcTarget.OthersBuffered, team);
        }

        [PunRPC]
        void RPC_SendTeam(int team)
        {
            this.team = team;
        }
    }
}
