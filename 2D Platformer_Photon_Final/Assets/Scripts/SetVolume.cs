using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;    // Variable for Audio Mixer

    public void SetlevelBGM (float sliderValue)    // BGM Slider Value
    {
        // represents the sliderValue to a logarithm based of 10
        mixer.SetFloat("BGMMusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetlevelSFX(float sliderValue)    // SFX Slider Value
    {
        // represents the sliderValue to a logarithm based of 10
        mixer.SetFloat("SFXMusicVol", Mathf.Log10(sliderValue) * 20);
    }















}
