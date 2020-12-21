﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class StageFlowManager : MonoSingleton<StageFlowManager> , IStageEndNotifier ,IStageChangeObserver
{
    [SerializeField]
    private StageTimeCounter timeCounter;
    private List<IStageEndObserver> stageEndObservers = new List<IStageEndObserver>();

    public float RequireTime
    {
        get => timeCounter.RequireTime;
    }
    public float Count
    {
        get => timeCounter.Count;
    }

    public void NotifyEnd()
    {
        timeCounter.EndStage();
    }

    public void ChangeStage(StageInfoContainer stage)
    {
        timeCounter.ChangeStage(stage);
        foreach (var a in stageEndObservers)
            if(a != null)
               a.EndStage();
    }

    private void EscapeStage()
    {
        if (timeCounter.RequireTime <= 0 && Input.GetKeyDown(KeyCode.E))
            NotifyEnd();
    }

    public void Update()
    {
        timeCounter.UpdateTime();
        EscapeStage();
    }

    private void Awake()
    {
        var observers = FindObjectsOfType(typeof(IStageEndObserver));
        foreach (var a in observers)
            if (a is IStageEndObserver)
                stageEndObservers.Add(a as IStageEndObserver);
    }
}



[Serializable]
public class StageTimeCounter : IStageChangeObserver, IStageEndObserver
{
    private bool isPlaying = true;
    public float RequireTime
    {
        private set;
        get;
    }
    public float Count
    {
        private set;
        get;
    }


    public void ChangeStage(StageInfoContainer stage)
    {
        isPlaying = true;
        RequireTime = stage.RequireSurviveTime;
        Count = 0;
    }

    public void EndStage()
    {
        isPlaying = false;
    }

    public void UpdateTime()
    {
        if (!isPlaying)
            return;
        RequireTime -= Time.deltaTime;
        RequireTime = RequireTime <= 0 ? 0 : RequireTime;
        Count += Time.deltaTime;
    }
}