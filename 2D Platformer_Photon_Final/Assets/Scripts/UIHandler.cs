using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Connected to lobby");
        //createRoomTF.text = PhotonNetwork.CurrentLobby.IsDefault.ToString();      // Check if Lobby is true.
    }

    public void OnClick_JoinRoom()
    {
        print(joinRoomTF.text);
        PhotonNetwork.JoinRoom(joinRoomTF.text);
        print("joined");
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(PhotonNetwork.CurrentRoom.Name + PhotonNetwork.CurrentRoom.PlayerCount);
        print("Successfully joined room!");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("Failed to join room" + returnCode + "Message" + message);
    }




}
