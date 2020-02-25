using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedPlayer : MonoBehaviour {

    public GameObject CurrentPlayer_PREFAB;

    public GameObject CurrentPlayers_GRID;

    //Called from GameManager
    public void AddLocalPlayer()
    {
        GameObject obj = Instantiate(CurrentPlayer_PREFAB, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(CurrentPlayers_GRID.transform, false);
        obj.GetComponentInChildren<Text>().text = "YOU: " + PhotonNetwork.NickName;
        obj.GetComponentInChildren<Text>().color = Color.green;
    }


    //Called from GameManager
    [PunRPC]
    public void UpdatePlayerList(string name)
    {
        GameObject obj = Instantiate(CurrentPlayer_PREFAB, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(CurrentPlayers_GRID.transform, false);
        obj.GetComponentInChildren<Text>().text = name;
    }


    //Called from GameManager
    public void RemovePlayerList(string name)
    {
        foreach (Text playerName in CurrentPlayers_GRID.GetComponentsInChildren<Text>())
        {
            if (name == playerName.text)
                Destroy(playerName.transform.parent.gameObject);
        }
    }
}
