using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;

    public Image hearts1, hearts2, hearts3;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
}
