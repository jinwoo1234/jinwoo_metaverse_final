using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class FinalNetworkManager : MonoBehaviourPunCallbacks
{
    public Transform _spawnPoint1;
    public Transform _spawnPoint2;

    public GameObject _p1Me;
    public GameObject _p2Me;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(800, 600, false);
        int playerNumber;

        if(PhotonNetwork.PlayerList.Length == 0)
        {
            print("p1 start");
            playerNumber = 1;
            
        } else if(PhotonNetwork.PlayerList.Length == 1)
        {
            print("p2 start");
            playerNumber = 2;
        } else
        {
            return;
        }

        string PlayerName = "Player " + playerNumber;
        PhotonNetwork.NickName = PlayerName;

        //start connecting
        print("Starting Connect Process...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Master Server");
        RoomOptions ro = new RoomOptions()
        {
            MaxPlayers = 2
        };

        PhotonNetwork.JoinOrCreateRoom("Room", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            print(PhotonNetwork.PlayerList.Length);
            print(PhotonNetwork.NickName + " has joined the Room");
            if (PhotonNetwork.PlayerList.Length == 1)
            {
                _p1Me.SetActive(true);
                PhotonNetwork.Instantiate("SoccerPlayer1", _spawnPoint1.position, Quaternion.LookRotation(_spawnPoint1.forward));
            }
            else
            {
                _p2Me.SetActive(true);
                PhotonNetwork.Instantiate("SoccerPlayer2", _spawnPoint2.position, Quaternion.LookRotation(_spawnPoint2.forward));
            }
        }
    }
}
