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
    [SerializeField] public AudioSource tickingSource;


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
    public AudioClip timesAlmostUp;
    public AudioClip timesUp;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayTicking()
    {
        tickingSource.Play();
    }

    public void StopTicking()
    {
        tickingSource.Stop();
    }

    public void PlayButtonHover()
    {
        sfxSource.PlayOneShot(hoverButton);
    }

    public void PlayButtonPress()
    {
        sfxSource.PlayOneShot(buttonClick);
    }
}
