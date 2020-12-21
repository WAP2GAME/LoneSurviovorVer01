using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerObjectStat : ObjectStat , IStageEndNotifier 
{
    private List<IStageEndObserver> stageEndObservers = new List<IStageEndObserver>();
    private bool IsGameOver(float damage)
    {
        return stat.HealthPoint - damage <= 0;
    }
    public override void TakeDamage(float damage)
    {
        if (IsGameOver(damage))
        {
            NotifyEnd();
            return;
        }
        base.TakeDamage(damage);
    }

    public void NotifyEnd()
    {
        foreach (var a in stageEndObservers)
            a.EndStage();
    }

    public bool AddObserver(IStageEndObserver observer)
    {
        if (stageEndObservers.Contains(observer))
            return false;
        stageEndObservers.Add(observer);
        return true;
    }

    public void DeleteObserver(IStageEndObserver observer)
    {

    }

    protected new void OnEnable()
    {
        base.OnEnable();
        var observer = FindObjectsOfType(typeof(IStageEndObserver));
        foreach (var a in observer)
            if(a is IStageEndObserver)
               stageEndObservers.Add(a as IStageEndObserver);
    }
}

