using UnityEngine;
using System.Collections;
using System.Security.Claims;
using System.Runtime.InteropServices.ComTypes;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay;
    private bool isSpawn = true;
    float spawnTimer = 0f;
    WaitForSeconds seconds;

    Vector3[] positions = new Vector3[6];
    private void Start()
    {
        seconds = new WaitForSeconds(spawnDelay); //  spawndelay를 인자로받아서 waitforseconds 객체가 생성/    
        CreatePositions();
        StartCoroutine(spawnEnemy());
    }
    private void CreatePositions()
    {
        float viewPosY = Random.Range(0, 10);
        float viewPosX = Random.Range(0, 10);
        for (int i = 0; i < positions.Length; i++)
        {
            viewPosX = Random.Range(0, 10);
            Vector3 viewPos = new Vector3(viewPosX, viewPosY, 0);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);  //뷰포트를 월드좌표로 바꾸어주는 함수
            worldPos.z = 0f;
            positions[i] = worldPos;
            print(worldPos);
        }
    }

    private IEnumerator spawnEnemy()
    {
        while (true)

        {
            if (isSpawn == true)
            {
                int rand = Random.Range(0, positions.Length);
                GameObject enemy = Instantiate(enemyPrefab, 
                                new Vector3(Random.Range(-100, 100), Random.Range(0, 5), Random.Range(-100, 100)), Quaternion.identity); //게임오브젝트 생성함수생성함수
                enemy.GetComponent<EnemyAI>().ZombieSetting();

            }
            yield return seconds;
        }
    }
}
