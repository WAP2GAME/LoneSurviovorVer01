using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class StageTimeCounter :  IStageChangeObserver, IStageEndObserver
{
    [SerializeField] 
    private Text PlayCountText;
    private bool isPlaying = true;
    [SerializeField] 
    private Text RequireCountText;
    [SerializeField]
    private GameObject escpaeInformText = null;
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
        RequireCountText.text = "RequireTime :" + "" + string.Format("{0:0.00}", Mathf.Round((float)RequireTime * 100) * 0.01f) + ""; //string.Format으로 소수점 둘째자리까지 고정해서 fix
        Count += Time.deltaTime;
        PlayCountText.text = "playTime : " + (int)Count + "";

        escpaeInformText.SetActive(RequireTime <= 0);
    }
}
