using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageTimeUI : MonoBehaviour ,IStageChangeObserver , IStageEndObserver
{
    [SerializeField]
    private Text playCountText;
    [SerializeField]
    private Text requireCountText;
    [SerializeField]
    private Text escpaeInformText = null;

    public void RefreshTimeUI()
    {
        if (StageFlowManager.Instance.IsOnStage)
        {
            var requireTime = StageFlowManager.Instance.RequireTime;
            var count = StageFlowManager.Instance.Count;
            requireCountText.text = "RequireTime :" + "" + string.Format("{0:0.00}", Mathf.Round((float)requireTime * 100) * 0.01f) + "";
            playCountText.text = "playTime : " + (int)count + "";
            escpaeInformText.gameObject.SetActive(requireTime <= 0);
        }
    }

    public void ChangeStage(StageInfoContainer stage)
    {
        gameObject.SetActive(true);
    }

    public void EndStage()
    {
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        RefreshTimeUI();
    }
}
