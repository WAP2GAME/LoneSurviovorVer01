using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Security.Claims;
//using System.Runtime.InteropServices.ComTypes;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ZombieList;
    [SerializeField] private float SpawnDelay;
    private bool isSpawn = true;
    
    //private GameObject Zombies;
    //Vector3[] positions = new Vector3[6];
    private void Start()
    {
        StartCoroutine(EnemySpawn());
        //CreatePositions();
    }
    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            float X = Random.Range(-100, 100);
            float Y = Random.Range(0, 5);
            float Z = Random.Range(-100, 100);

            if (isSpawn)
            {
                // int rand = Random.Range(0, positions.Length);
                GameObject Zombies = Instantiate(ZombieList[Random.Range(0,ZombieList.Count-1)], new Vector3(X, Y, Z), Quaternion.identity); //게임오브젝트 생성함수생성함수
                Zombies.SetActive(true);
                Debug.Log(Zombies.name);
                Debug.Log(Zombies.activeSelf);
                Zombies.GetComponent<EnemyAI>().ZombieSetting();
            }
        yield return new WaitForSeconds(SpawnDelay);
        } 
        
    }
    /*private void CreatePositions()
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
    }*/
}