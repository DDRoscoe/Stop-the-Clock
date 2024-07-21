using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Doozy.Runtime.UIManager.Components;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private UISlider masterSlider;
    [SerializeField] private UISlider musicSlider;
    [SerializeField] private UISlider sfxSlider;

    private AudioManager audioManagerScript;


    private void Start()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }


    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }

    public void TestSFX()
    {
        audioManagerScript.PlaySFX(audioManagerScript.coin);
    }
}