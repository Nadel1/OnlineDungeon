/*
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


        // Start Method
        private void Start()
        {
            DDOL = GameObject.FindGameObjectWithTag("DDOL");
            teamMembers = DDOL.GetComponent<Launcher>().GetTeamMembers();
            List<Player> team0 = teamMembers[0];
            List<Player> team1 = teamMembers[teamMembers.Count-1];
            int team;

            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {


                Player p=PhotonNetwork.LocalPlayer;
                team = team0.Contains(p) ? 0 : 1;
                GameObject[] spawnPos;
                Transform spawnAt=null;

                spawnPos=(team==0)? team1Spawn.GetComponent<SpawningPos>().spawnPos: team2Spawn.GetComponent<SpawningPos>().spawnPos;

                spawnPos = team1Spawn.GetComponent<SpawningPos>().spawnPos;
                spawnAt= spawnPos[0].transform;
                /*for (int i = 0; i < spawnPos.Length; i++)
                {
                    if (spawnPos[i] == null)
                    {
                        spawnAt = spawnPos[i].transform;
                        break;
                    }
                }*/

                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("Instantiating Player 1");
                    player1 = PhotonNetwork.Instantiate("Player", spawnAt.position, spawnAt.rotation, 0);
                }
                else
                {
                    player2 = PhotonNetwork.Instantiate("Player", spawnAt.position, spawnAt.rotation, 0);
                }
            }
        }
        // Update Method
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
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

    }
}
