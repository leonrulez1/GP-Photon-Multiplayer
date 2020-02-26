using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject UserNameScreen, ConnectScreen;

    [SerializeField]
    private GameObject CreateUserNameButton;

    [SerializeField]
    private InputField UserNameInput, CreateRoomInput, JoinRoomInput;

    #region MainStart
    public GameObject MainStartMenu;
    bool hasMainStart;
    DisManager dm;

    /*
    void Awake()
    {
        //Connect to server using the PhotonServerSettings
        //PhotonNetwork.ConnectUsingSettings();
        mainStart = false;
    }*/

    void Start()
    {
        dm = FindObjectOfType<DisManager>();
    }

    void Update()
    {     
        if (Input.GetKeyDown(KeyCode.Space) && hasMainStart == false && dm.disconnectedFromTheGame == false)
        {
            PhotonNetwork.ConnectUsingSettings();
            hasMainStart = true;
            MainStartMenu.SetActive(false);
        }
    }
    #endregion

    // Called when the client is connected to the Master Server and ready for matchmaking and other tasks.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!!!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    // Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate.
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!!!");
        UserNameScreen.SetActive(true);
    }

    // Called when the LoadBalancingClient entered a room, no matter if this client created it or simply joined.
    public override void OnJoinedRoom()
    {
        // Play game scene
        PhotonNetwork.LoadLevel(1);
    }

    #region UIMethods

    // Called after entered name and click on Create Name button
    public void OnClick_CreateNameBtn()
    {
        PhotonNetwork.NickName = UserNameInput.text;
        UserNameScreen.SetActive(false);
        ConnectScreen.SetActive(true);
    }

    // Make sure the user name follows certain format
    public void OnNameField_Changed()
    {
        if (UserNameInput.text.Length >= 2)
        {
            CreateUserNameButton.SetActive(true);
        }
        else
        {
            CreateUserNameButton.SetActive(false);
        }
    }

    // Called when click on Join Room button
    public void Onclick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(JoinRoomInput.text, ro, TypedLobby.Default);
    }

    // Called when click on Create Room button
    public void Onclick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRoomInput.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

    #endregion
}
