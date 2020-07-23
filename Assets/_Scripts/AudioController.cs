﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public List<DictAudio> AllAudios;
    private AudioSource AudioSource;

    private void Awake()
    {
        if (Instance)
        {
            return;
        }
        Instance = this;
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void ChangeAudio(string audioName)
    {
        AudioClip selectedClip = AllAudios.Where(entry => entry.Name == audioName).FirstOrDefault().Audio;
        AudioSource.clip = selectedClip;
        AudioSource.Play();
    }
}

[Serializable]
public class DictAudio
{
    public string Name;
    public AudioClip Audio;
}
