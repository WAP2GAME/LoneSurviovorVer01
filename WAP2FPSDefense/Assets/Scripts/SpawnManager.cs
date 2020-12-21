using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour, IStageChangeObserver, IStageEndObserver
{
    [SerializeField] private List<GameObject> ZombieList;
    [SerializeField] private float SpawnDelay;

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
        spawnpos.Clear();
        var newList = stage.EnemySpawnPosList;
        for (int q = 0; q < newList.Count; q++)
            spawnpos.Add(newList[q]); 
    }

    public void EndStage()
    {
        stageEndSpawn = true;
        if (stageEndSpawn)
        {

        }
        
    }
    private IEnumerator EnemySpawn()
    {
        int i = 0;

        while (true)
        {
            if (isSpawn && !stageEndSpawn)
            {
                GameObject Zombies = Instantiate(ZombieList[Random.Range(0, ZombieList.Count - 1)], spawnpos[i], Quaternion.identity);
                Zombies.GetComponent<EnemyAI>().ZombieSetting();
               if (++i % spawnpos.Count == 0)
                {
                    i = 0;
                }
            }

        yield return new WaitForSeconds(SpawnDelay);
        } 
    }

}