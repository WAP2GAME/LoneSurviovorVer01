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

    private bool IsAllStageFinished
    {
        get
        {
            int cnt =0;
            foreach (var stage in stageList)
                if (stage.IsFinished)
                    cnt++;

            return cnt == stageList.Count;
        }
    }

    public void MoveStage()
    {
        var nextStage = GetRandomStage();
        CurrentStage = nextStage;
        defendObj.transform.position = nextStage.DefendObjSpawnPos;
        defendObj.SetActive(true);
        player.transform.position = nextStage.PlayerSpawnPos;
        GunController.isActivate = true;
        Cursor.visible = false;
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
        GunController.isActivate = false;
        CurrentStage.IsFinished = true;
        Cursor.visible = true;
        FinishedStageCnt++;
    }

    private StageInfoContainer GetRandomStage()
    {
        if (IsAllStageFinished)
            EnableAllStage();

        StageInfoContainer stage = null;
        while(stage == null)
        {
            int idx = Random.Range(0, stageList.Count);
            if (!stageList[idx].IsFinished)
                stage = stageList[idx];
        }

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
            if (observer is IStageChangeObserver)
                stageChangeObservers.Add(observer as IStageChangeObserver);
        }
    }
}
