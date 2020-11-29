using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManger : MonoSingleton<GameStageManger>
{
    [SerializeField]
    private List<StageInfoContainer> stageList = new List<StageInfoContainer>();
    [SerializeField]
    private GameObject playerObj = null;
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

    }

    private void SetObjectPositionsOf(StageInfoContainer stage)
    {
        playerObj.transform.position = stage.PlayerSpawnPos;
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
