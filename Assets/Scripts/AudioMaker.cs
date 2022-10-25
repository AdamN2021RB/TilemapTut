using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaker : MonoBehaviour
{

     public AudioClip BackgroundMusic;

    public AudioSource musicSource;

    void Start()
    {
        musicSource.clip = BackgroundMusic;
            musicSource.Play();
    }

    //Found on OpenGameArt. Music created by "Zane Little Music"
}
