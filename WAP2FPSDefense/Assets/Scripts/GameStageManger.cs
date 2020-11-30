using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManger : MonoSingleton<GameStageManger>
{
    [SerializeField]
    private List<StageInfoContainer> stageList = new List<StageInfoContainer>();
    private List<IStageChage> stageChangeObservers = new List<IStageChange>();
    public int FinishedStageCnt
    {
        private set;
        get;
    }
    public StageInfoContainer CurrentStage
    {
        private set;
        get;
    }

    public void MoveStage()
    {
        var nextStage = GetRandomStage();
        foreach (var observers in stageChangeObservers)
            observers.ChangeStage(nextStage);
    }

    private StageInfoContainer GetRandomStage()
    {
        if (++FinishedStageCnt >= stageList.Count)
            EnableAllStage();

        StageInfoContainer stage = null;
        while(stage == null)
        {
            int idx = Random.Range(0, stageList.Count);
            if (!stageList[idx].IsFinished)
                stage = stageList[idx];
        }

        stage.IsFinished = true;
        return stage;
    }

    private void EnableAllStage()
    {
        foreach (var stage in stageList)
            stage.IsFinished = false;
    }
}
