using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkController: MonoBehaviourPunCallbacks
{
    //public string versionName = "0.1";

    public Button BtnConnectMaster;
    public Button BtnConnectRoom;

    public GameObject connectedToTheServer;
    public GameObject disconnectedFromTheServer;

    public bool TriesToConnectToMaster;
    public bool TriesToConnectToRoom;
   
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
    }

    void Update()
    {
        if (BtnConnectMaster != null)
            BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        /*
        if (BtnConnectRoom != null)
            BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
        */  
    }

    public void OnClickConnectToMaster()
    {
        /*
        PhotonNetwork.OfflineMode = false;           //true would "fake" an online connection
        PhotonNetwork.NickName = "PlayerName";       //to set a player name
        PhotonNetwork.AutomaticallySyncScene = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = "v1";            //only people with the same game version can play together
        */
        TriesToConnectToMaster = true;       
        PhotonNetwork.ConnectUsingSettings();           //automatic connection based on the config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset

    }

    public override void OnConnectedToMaster()      // Positive or Connected
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        connectedToTheServer.SetActive(true);
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to Master!");
    }

    public override void OnDisconnected(DisconnectCause cause)  // Negative or Disconnected
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        disconnectedFromTheServer.SetActive(true);
        Debug.Log(cause);
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        TriesToConnectToRoom = true;
        //PhotonNetwork.CreateRoom("Jesper's Game 1"); //Create a specific Room - Error: OnCreateRoomFailed
        //PhotonNetwork.JoinRoom("Jesper's Game 1");   //Join a specific Room   - Error: OnJoinRoomFailed  
        PhotonNetwork.JoinRandomRoom();               //Join a random Room     - Error: OnJoinRandomRoomFailed  
    }

    #region Moved to UIHandler Script

    /*
    public override void OnJoinRandomFailed(short returnCode, string message)       // Negative or Failed to join Room
    {
        base.OnJoinRandomFailed(returnCode, message);
        //no room available
        //create a room (null as a name means "does not matter")
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        base.OnCreateRoomFailed(returnCode, message);
        TriesToConnectToRoom = false;
    }

    public override void OnJoinedRoom()     // Positive or Joined Room
    {
        base.OnJoinedRoom();
        TriesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("Level1");
    }*/

    #endregion

}