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
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private GameObject teamPanel;

        [SerializeField]
        private Text feedbackText;

        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        bool isConnecting;

        string gameVersion = "1";

        [Space(10)]
        [Header("Custom Variables")]
        public InputField playerNameField;
        public InputField roomNameField;

        [Space(5)]
        public Text playerStatus;
        public Text connectionStatus;

        [Space(5)]
        public GameObject roomJoinUI;
        public GameObject buttonLoadArena;
        public GameObject buttonJoinRoom;

        string playerName = "";
        string roomName = "";

        // Start Method
        private void Start()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Connecting to Photon Network");

            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);

            ConnectToPhoton();
        }

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Helper Methods
        public void SetPlayerName(string name)
        {
            playerName = name;
        }

        public void SetRoomName(string name)
        {
            roomName = name;
        }
        // Tutorial Methods
        void ConnectToPhoton()
        {
            connectionStatus.text = "Connecting...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        // Photon Methods
        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                playerName = playerNameField.text;
                PhotonNetwork.LocalPlayer.NickName = playerName;

                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomNameField.text);
                RoomOptions roomOptions = new RoomOptions();
                SetRoomName(roomNameField.text);
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
        }

        public void SetTeams()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                controlPanel.SetActive(false);
                teamPanel.SetActive(true);
                
            }
            else
            {
                playerStatus.text = "Minimum 2 Players required to assign Teams!";
            }
        }
        public void LoadArena()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                PhotonNetwork.LoadLevel("Dungeon");
            }
            else
            {
                playerStatus.text = "Minimum 2 Players required to Load Arena!";
            }
        }

        public override void OnConnected()
        {
            base.OnConnected();

            connectionStatus.text = "Connected to Photon!";
            connectionStatus.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            Debug.LogError("Disconnected. Please check your Internet connection");
        }

        private List<Player> playerList = new List<Player>();
        public override void OnJoinedRoom()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                buttonLoadArena.SetActive(true);
                buttonJoinRoom.SetActive(false);
                playerStatus.text = "You are the Lobby Leader";
            }
            else
            {
                playerStatus.text = "Connected to Lobby";
            }
        }
    }
}
