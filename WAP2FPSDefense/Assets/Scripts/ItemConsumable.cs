using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemConsumable : ItemBase
{
    [SerializeField]
    protected float baseCoolTime;
    [SerializeField]
    protected int retainCntLimit;
    bool isOnCoolTime;

    public float BaseCoolTime
    {
        get => baseCoolTime;
    }
    public int RetainCntLimit
    {
        get => retainCntLimit;
    }

    public abstract void ExecuteItemEffect();
    protected IEnumerator CountCoolTime()
    {
        if(isOnCoolTime)
            yield break;

        isOnCoolTime = true;
        float second = 0;
        while(second< baseCoolTime)
            second++;
        isOnCoolTime = false;
    }
}
