using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManger : MonoSingleton<GameStageManger> , IStageChangeNotifier , IStageEndObserver
{
    [SerializeField]
    private List<StageInfoContainer> stageList = new List<StageInfoContainer>();
    [SerializeField]
    private List<GameObject> stageChangeObserverObjs;
    private List<IStageChangeObserver> stageChangeObservers = new List<IStageChangeObserver>();

    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private GameObject defendObj = null;

    public GameObject Player
    {
        get => player;
    }
    public GameObject DefendObj
    {
        get => defendObj;
    }

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
        CurrentStage = nextStage;
        defendObj.transform.position = nextStage.DefendObjSpawnPos;
        defendObj.SetActive(true);
        player.transform.position = nextStage.PlayerSpawnPos;
        player.SetActive(true);
        Notify(nextStage);
    }

    public void Notify(StageInfoContainer stage)
    {
        foreach (var a in stageChangeObservers)
            a.ChangeStage(stage);
    }

    public void EndStage()
    {
        defendObj.SetActive(false);
        player.SetActive(false);
    }

    private StageInfoContainer GetRandomStage()
    {
        if (++FinishedStageCnt >= stageList.Count)
        {
            FinishedStageCnt = 0;
            EnableAllStage();
        }

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


    private void Awake()
    {
        foreach (var a in stageChangeObserverObjs)
        {
            var observer = a.GetComponent<IStageChangeObserver>();
            if(observer is IStageChangeObserver)
               stageChangeObservers.Add(observer as IStageChangeObserver);
        }
    }
}
