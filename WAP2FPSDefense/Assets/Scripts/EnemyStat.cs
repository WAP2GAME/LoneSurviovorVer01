using System.Collections;
using System.Collections.Generic;
using UnityEngine;





class EnemyStat : ObjectStat
{
    [SerializeField]
    private Stat overallStat;
    [SerializeField]
    private ObjectStat head;
    [SerializeField]
    private ObjectStat leg;
    [SerializeField]
    private ObjectStat arm;
    [SerializeField]
    private ObjectStat body;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}

