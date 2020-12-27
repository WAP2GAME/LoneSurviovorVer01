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
        stat.TakeDamage(damage);
        if (stat.IsDead)
        {
            ScoreManager.Instance.AddCoin(10);
            ScoreManager.Instance.AddScore(10);
            GameStageManger.Instance.Player.GetComponentInChildren<GunController>().currentGun.carryBulletCount += 30;
            Destroy(gameObject);
        }
    }
}

