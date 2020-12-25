using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[RequireComponent(typeof(AudioSource))]
public class ObjectAudio : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> auidos;
    [SerializeField]
    protected AudioSource audioSource;
    public bool IsPlayeing
    {
        protected set { IsPlayeing = value; }
        get => audioSource.isPlaying;
    }

    public virtual void Play(int idx)
    {
        if (idx >= auidos.Count)
            return;

        if (IsPlayeing)
            audioSource.Stop();
        audioSource.clip = auidos[idx];
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

