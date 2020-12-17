using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour, IStageEndObserver
{
    [Header("Buttons")]
    [SerializeField]
    private Button exitBtn = null;
    [SerializeField]
    private Button shopBtn = null;
    [SerializeField]
    private Button stageJumpBtn = null;

    [Header("Scroe Show")]
    [SerializeField]
    private Text surviveTimeScore = null;
    [SerializeField]
    private Text killCountScore = null;
    [SerializeField]
    private Text resultText = null;


    public void SetScore(float surviveTime, float killCnt)
    {
        surviveTimeScore.text = surviveTime.ToString();
        killCountScore.text = killCnt.ToString();
    }

    public void SetResultAsWon(bool isWon)
    {
        if (isWon)
            resultText.text = "Congraturation";
        else
            resultText.text = "Game Over";
        shopBtn.gameObject.SetActive(isWon);
        stageJumpBtn.gameObject.SetActive(isWon);
    }

    public void EndStage()
    {
        var lastStage = GameStageManger.Instance.CurrentStage;

    }
}
