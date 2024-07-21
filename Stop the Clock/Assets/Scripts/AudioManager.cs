using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip hoverButton;
    public AudioClip buttonClick;
    public AudioClip playButtonClick;
    public AudioClip coin;
    public AudioClip comboBreak;
    public AudioClip ticking;
    public AudioClip countdown;
    public AudioClip begin;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
