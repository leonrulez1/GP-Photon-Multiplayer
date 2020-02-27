using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    
    public static AudioManager Instance
    {
        get { return instance; }
    }

    // When game is started, it will check for instance
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }           

        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);         // Game Object won't get destroy on loading of other scenes
    }







}
