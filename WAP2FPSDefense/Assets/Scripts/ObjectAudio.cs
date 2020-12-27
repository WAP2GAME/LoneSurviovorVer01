using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[RequireComponent(typeof(AudioSource))]
public class ObjectAudio : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> audios;
    [SerializeField]
    protected AudioSource audioSource;
    public bool IsPlaying
    {
        protected set { IsPlaying = value; }
        get => audioSource.isPlaying;
    }
    public int AudioCnt
    {
        get => audios.Count;
    }

    public virtual void Play(int idx)
    {
        if (idx >= audios.Count)
            return;

        if (IsPlaying)
            audioSource.Stop();
        audioSource.clip = audios[idx];
        audioSource.Play();
    }

    public virtual void Play()
    {
        if (audioSource.clip != null)
            audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}

