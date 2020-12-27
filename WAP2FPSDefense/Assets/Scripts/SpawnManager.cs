using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour, IStageChangeObserver, IStageEndObserver
{
    [SerializeField] private List<GameObject> ZombieList;
    [SerializeField] private float SpawnDelay;
    private List<GameObject> spawnedZombies = new List<GameObject>();

    public Vector3 vector;
    private bool isSpawn = true;
    private bool stageEndSpawn = true;
    public List<Vector3> spawnpos = new List<Vector3>();

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    public void Update()
    {
        for (int i = 0; i < spawnpos.Count; i++)
        {
            vector = spawnpos[i];
        }
    }

    public void ChangeStage(StageInfoContainer stage)
    {
        stageEndSpawn = false;
        spawnpos.Clear();
        var newList = stage.EnemySpawnPosList;
        for (int q = 0; q < newList.Count; q++)
            spawnpos.Add(newList[q]); 
    }

    public void EndStage()
    {
        stageEndSpawn = true;
        foreach (var a in spawnedZombies)
            Destroy(a);
        spawnedZombies.Clear();
    }
    private IEnumerator EnemySpawn()
    {
        int i = 0;

        while (true)
        {
            if (isSpawn && !stageEndSpawn)
            {
                GameObject zombie = Instantiate(ZombieList[Random.Range(0, ZombieList.Count - 1)], spawnpos[i], Quaternion.identity);
                zombie.GetComponent<EnemyAI>().ZombieSetting();
                spawnedZombies.Add(zombie);
               if (++i % spawnpos.Count == 0)
                    i = 0;
            }

        yield return WaitTime.GetWaitForSecondOf(SpawnDelay);
        } 
    }

}