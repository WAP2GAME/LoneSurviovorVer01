using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo",menuName = "Scriptable Object/Stage Info")]
class StageInfoContainer : ScriptableObject
{
    [SerializeField]
    private Vector3 playerSpawnPos;
    [SerializeField]
    private Vector3 defendObjSpawnPos;

    [SerializeField]
    private List<Vector3> enemySpawnPosList = new List<Vector3>(10);

    [SerializeField]
    private float requireSurviveTime = 30f;
    private bool isFinished = false;

    public Vector3 PlayerSpawnPos
    {
        get => playerSpawnPos;
    }
    public Vector3 DefendObjSpawnPos
    {
        get => defendObjSpawnPos;
    }
    public ReadOnlyCollection<Vector3> EnemySpawnPosList
    {
        get => enemySpawnPosList.AsReadOnly();
    }
    public float RequireSurviveTime
    {
        get => requireSurviveTime;
    }
    public bool IsFinished
    {
        get;
        set;
    }
}

