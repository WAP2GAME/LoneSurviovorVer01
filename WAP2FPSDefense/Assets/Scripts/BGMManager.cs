using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(AudioSource))]
public class BGMManager : ObjectAudio ,IStageChangeObserver ,IStageEndObserver
{
    public void ChangeStage(StageInfoContainer stage)
    {
        Stop();
        Play(1);
    }
    public void EndStage()
    {
        Stop();
        Play(2);
    }

    public void Start()
    {
        Play(0);
    }
}

