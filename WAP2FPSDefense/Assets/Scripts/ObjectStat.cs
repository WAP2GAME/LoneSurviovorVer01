using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    protected float baseHealthPointMax;
    protected float healthPointMax;
    [SerializeField]
    protected float healthPoint;
    protected float healthPointCoefficient = 1f;
    public float HealthPointMax
    {
        get => healthPointMax;
    }
    public float HealthPoint
    {
        get => healthPoint;
    }
    public bool IsDead
    {
        get
        {
            if (healthPoint <= 0)
                return true;
            else
                return false;
        }
    }

    public Stat(int maxHP)
    {
        baseHealthPointMax = maxHP;
        InitStats();
    }

    #region user function
    public void InitStats()
    {
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = healthPointMax;
    }

    public void InitStatsAsZero()
    {
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = 0;
    }

    public virtual void TakeDamage(float damage)
    {
        healthPoint -= damage;
    }

    public virtual void TakeHeal(float heal)
    {
        float leftHP = healthPointMax - healthPoint;
        if (leftHP != 0)
        {
            if (leftHP < heal)
            {
                healthPoint = healthPointMax;
                heal -= leftHP;
            }
            else
            {
                healthPoint += heal;
                return;
            }
        }
    }

    public void AdjustHealthPoint(float ratio)
    {
        float presntHPratioByMaxHP = healthPoint / healthPointMax;

        healthPointCoefficient += ratio;
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = (int)(healthPointMax * presntHPratioByMaxHP);
    }

    public void AdjustHealthPoint(int value)
    {
        float presntHPratioByMaxHP = healthPoint / healthPointMax;

        baseHealthPointMax += value;
        healthPointMax = (int)(baseHealthPointMax * healthPointCoefficient);
        healthPoint = (int)(healthPointMax * presntHPratioByMaxHP);
    }
    #endregion
}


public class ObjectStat : MonoBehaviour
{
    #region variables
    [SerializeField]
    private Stat stat;

    public float HealthPoint
    {
        get => stat.HealthPoint;
    }
    public float HealthPointMax
    {
        get => stat.HealthPointMax;
    }
    #endregion
    #region user function
    public virtual void TakeDamage(float damage)
    {
        stat.TakeDamage(damage);
        if (stat.IsDead)
            gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage, EBodyParts hitPart)
    {
        TakeDamage(damage);
    }

    public virtual void TakeHeal(int heal)
    {
        stat.TakeHeal(heal);
    }

    public void AdjustHealthPoint(int value)
    {
        stat.AdjustHealthPoint(value);
    }

    public void AdjustHealthPoint(float ratio)
    {
        stat.AdjustHealthPoint(ratio);
    }
    #endregion
    private void OnEnable()
    {
        stat.InitStats();
    }
}