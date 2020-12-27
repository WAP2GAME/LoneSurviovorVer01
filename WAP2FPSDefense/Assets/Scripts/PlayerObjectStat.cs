using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


class PlayerObjectStat : ObjectStat , IStageEndNotifier 
{
    [SerializeField]
    private List<GameObject> stageEndObserverObjs;
    private List<IStageEndObserver> stageEndObservers = new List<IStageEndObserver>();

    [SerializeField]
    private HitMotionAnimator hitMotionAnimator;
    private ObjectAudio audioManager;
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
        audioManager.Play(0);
        base.TakeDamage(damage);
        //if (!hitMotionAnimator.IsOn)
         //   StartCoroutine(hitMotionAnimator.AnimateHitEvent());
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
        audioManager = GetComponent<ObjectAudio>();
    }
}


[Serializable]
public class HitMotionAnimator
{
    [SerializeField]
    private Transform obj;
    private float elapsedTime = 0f;
    private Vector3 originPos;
    private Vector3 direction;

    public float power = 1f;
    public float animationTime = 1f;
    private bool isHalfTimePassed;

    private bool IsHalfTime
    {
        get
        {
            bool isTrue = elapsedTime <= (animationTime / 2 + Time.fixedDeltaTime) && elapsedTime >= (animationTime / 2 - Time.fixedDeltaTime);
            isHalfTimePassed = isTrue;
            return isTrue;
        }
    }

    public bool IsOn
    {
        private set;
        get;
    }

    public HitMotionAnimator()
    {
        IsOn = false;
    }

    

    public IEnumerator AnimateHitEvent()
    {
        if (obj == null)
            yield break;

        direction = GetRandomDirecion();
        IsOn = true;
        elapsedTime = 0;
        while (elapsedTime < animationTime)
        {
            if (!isHalfTimePassed && IsHalfTime)
                direction *= -1;

            obj.Translate(direction * power * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        IsOn = false;
    }

    private Vector3 GetRandomDirecion()
    {
        Vector3 dir = new Vector3();
        for (int i = 0; i < 3; i++)
            dir[i] = UnityEngine.Random.Range(0, 10f);

        return dir.normalized;
    }

    
}