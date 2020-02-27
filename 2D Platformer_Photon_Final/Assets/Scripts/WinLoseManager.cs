using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Lobby");
        Debug.Log("Back To Main Menu");
    }






}
