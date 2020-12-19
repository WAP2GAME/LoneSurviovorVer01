using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Math;
public class CountManager : MonoBehaviour
{
    //private CountManager countManager;
    public static CountManager Instance = null;
    public static CountManager instance
    {
        get
        {
            return Instance;
        }
    }
    [SerializeField] StageInfoContainer stageinfocontainer;
    [SerializeField] Text PlayCountText;
    public float Count = 0;
    [SerializeField] Text RequireCountText;
    private float RequireTime;

    void Awake()
    {
        //countManager = CountManager.Instance;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        RequireTime = stageinfocontainer.RequireSurviveTime;
    }
    public void FixedUpdate()
    {
        RequireTime -= Time.deltaTime;
        RequireCountText.text = "RequireTime :" + "" + string.Format("{0:0.00}",Mathf.Round((float)RequireTime*100)*0.01f) + ""; //string.Format으로 소수점 둘째자리까지 고정해서 fix

        if (RequireTime < 0)
        {
            RequireTime = 30;
        }
        Count += Time.deltaTime;
        PlayCountText.text = "playTime : " + (int)Count + "";

    }
}
