using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerObjectStat : ObjectStat , IStageEndNotifier 
{
    [SerializeField]
    private List<GameObject> stageEndObserverObjs;
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

    protected new void OnEnable()
    {
        base.OnEnable();
        foreach (var a in stageEndObserverObjs)
        {
            var observer = a.GetComponent<IStageEndObserver>();
            if (observer != null)
                stageEndObservers.Add(observer as IStageEndObserver);
        }
    }
}

