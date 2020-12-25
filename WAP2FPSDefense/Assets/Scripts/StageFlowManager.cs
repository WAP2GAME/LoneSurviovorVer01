using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class StageFlowManager : MonoSingleton<StageFlowManager> , IStageEndNotifier ,IStageChangeObserver
{
    [SerializeField]
    private StageTimeCounter timeCounter;
    [SerializeField]
    private List<GameObject> stageEndObserverObjs;
    private List<IStageEndObserver> stageEndObservers = new List<IStageEndObserver>();

    public float RequireTime
    {
        get => timeCounter.RequireTime;
    }
    public float Count
    {
        get => timeCounter.Count;
    }

    public bool IsOnStage
    {
        private set;
        get;
    }

    public void NotifyEnd()
    {
        timeCounter.EndStage();
        foreach (var a in stageEndObservers)
            if (a != null)
                a.EndStage();
    }

    public void ChangeStage(StageInfoContainer stage)
    {
        IsOnStage = true;
        timeCounter.ChangeStage(stage);
    }

    private void EscapeStage()
    {
        if (timeCounter.RequireTime <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            IsOnStage = false;
            NotifyEnd();
        }
    }

    public void Update()
    {
        timeCounter.UpdateTime();
        EscapeStage();
    }

    private void Awake()
    {
        foreach (var a in stageEndObserverObjs)
        {
            var observer = a.GetComponent<IStageEndObserver>();
            if (observer is IStageEndObserver)
                stageEndObservers.Add(observer as IStageEndObserver);
        }
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
