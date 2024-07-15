using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource, sfxSource;
    public Sound[] musicClips, sfxClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicClips, x=> x.name == name);

        if (s == null)
            Debug.Log("music not found");
        else
            musicSource.Play();

    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxClips, x=> x.name == name);

        if (s == null)
            Debug.Log("sfx not found");
        else
            sfxSource.PlayOneShot(s.clip);
        
    }

    public void LoopSFX(string name)
    {
        Sound s = Array.Find(sfxClips, x=> x.name == name);

        if (s == null)
            Debug.Log("sfx not found");
        else
        {
            sfxSource.loop = true;
            sfxSource.Play();
        }
        
    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxClips, x=> x.name == name);

        if (s == null)
            Debug.Log("sfx not found");
        else
            sfxSource.Stop();
        
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
