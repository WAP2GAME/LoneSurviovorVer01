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

    public abstract void Use();
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

[CreateAssetMenu(fileName = "PotionHeal",menuName ="Scriptable Object/Potion Heal",order = 1)]
public class ItemPotion : ItemConsumable
{
    [SerializeField]
    private int heal = 50;
    public override void Use()
    {
        ObjectStat stat;
        if (GameStageManger.Instance.Player != null && (stat = GameStageManger.Instance.Player.GetComponent<ObjectStat>()) != null)
            stat.TakeHeal(heal);
    }
}