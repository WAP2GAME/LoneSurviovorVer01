//using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI; //NavMesh
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    //EnemyTrace
    private NavMeshAgent agent = null;
    private SearchComponent searchcomponent = null;
    private Vector3 destination;
    public GameObject enemyTransform;
    private float dist;
    [SerializeField] private GameObject target;

    //AttackTarget
    [SerializeField] private bool HitDamage; // 공격
    [SerializeField] private bool HitSpeed;  // 공격속도
    [SerializeField] private float EnemyStr;
    private float Hitrange = 100f;
    Animator anim;
    private bool targetAttack;
    private bool isDead;
  

    private void Start()
    {
        //EnemyTrace
        searchcomponent = GetComponent<SearchComponent>();
        agent = GetComponent<NavMeshAgent>();

        //AttackTarget
        anim = GetComponent<Animator>();
        targetAttack = false;
        isDead = false;

        Hitrange += StageManager.instance.AddZombieHp;
        EnemyStr += StageManager.instance.AddZombiePower;

    }

    

    //void Update()
    //{
    //    //TraceEnemy();
    //}
    //public void TraceEnemy() // EnemyTrace
    //{

    //    if (Vector3.Distance(transform.position, target.transform.position) < Hitrange)
    //    {
    //        transform.LookAt(target.transform);
    //        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f);
    //        anim.SetTrigger("targetFind"); // Run애니메이션 실행
    //    }
    //}


    private IEnumerator ZombieAttack()
    {
        if(Vector3.Distance(transform.position, StageManager.instance.Player.transform.position) < 50) // 사정거리 안에 플레이어가 들어온다면
        {
            target = StageManager.instance.Player;
        }
        else
        {
            target = StageManager.instance.Nexus;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 0.1f);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f);


        if(Vector3.Distance(transform.position,target.transform.position) < 5)
        {
            anim.SetBool("TargetFind", false);
            anim.SetBool("targetAttack", true);
            print("공격시작");
            yield break;
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(ZombieAttack());
    }



    public void ZombieSetting()
    {
        //anim.SetTrigger("targetFind");
        StartCoroutine(ZombieAttack());
    }

}
        /*int size = searchcomponent.SearchedObjs.Count;
        GameObject[] objects = searchcomponent.SearchedObjs.ToArray(); // 큐에있는 모든 원소를 배열로 변환 ?
        foreach (GameObject distvar in objects)
        {
            if (searchcomponent.SearchedObjs.Count != 0) //주위에 적이있다면 !=0 이라면
            {
                enemyTransform = searchcomponent.SearchedObjs.Dequeue(); // 찾은 적들
                agent.transform.LookAt(target.transform.position); // 타겟을 바라봄
                anim.SetTrigger("targetFind");
                destination = enemyTransform.transform.position; //이동
                agent.destination = destination;  // searchcomponent 를 통해 찾는 위치로 agent가 이동 
                dist = Vector3.Distance(destination, agent.transform.position); //거리를 재는 변수
        
                
                
                if (dist <= 5f)
                {
                    //AttackTarget(enemyTransform);
                    agent.isStopped = true; // 거리가 50이하이면 타겟 멈춤
                }
            }
            else
            {
                agent.isStopped = false;
                anim.SetTrigger("targetFind");
                agent.destination = target.transform.position; //목표오브젝트를 향해감.
            }
        }
    }*/


    //obj.GetComponent<EntityStats>().TakeDamage(EnemyStr);

    /*public void AttackTarget(GameObject obj)
    {
        StartCoroutine("hittarget");

    }*/
    



    