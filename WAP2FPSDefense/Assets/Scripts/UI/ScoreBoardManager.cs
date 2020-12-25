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

    [SerializeField]
    private GameObject shopCanvas = null;
    public void SetScore()
    {
        surviveTimeScore.text = "You have survived "+((int)(StageFlowManager.Instance.Count)).ToString()+"Sec.";
        killCountScore.text = "Total kill Point :"+ ScoreManager.Instance.Score;
    }

    public void SetResult(bool isWon)
    {
        if (isWon)
            resultText.text = "Congraturation";
        else
            resultText.text = "Game Over";
        SetScore();
        shopBtn.gameObject.SetActive(isWon);
        stageJumpBtn.gameObject.SetActive(isWon);
    }

    public void EndStage()
    {
        gameObject.SetActive(true);
        var lastStage = GameStageManger.Instance.CurrentStage;
        SetResult(StageFlowManager.Instance.Count >= lastStage.RequireSurviveTime);
    }

    private void SetButtonFunc()
    {
        if (exitBtn)
            exitBtn.onClick.AddListener(Quit);
        if (stageJumpBtn)
            stageJumpBtn.onClick.AddListener(JumpStage);
        if (shopBtn && shopCanvas)
            shopBtn.onClick.AddListener(GoShop);
    }

    private void Quit()
    {
        Application.Quit(1);
    }

    private void JumpStage()
    {
        GameStageManger.Instance.MoveStage();
        gameObject.SetActive(false);
    }

    private void GoShop()
    {
        shopCanvas.SetActive(true);
    }

    private void Awake()
    {
        SetButtonFunc();
    }
}
