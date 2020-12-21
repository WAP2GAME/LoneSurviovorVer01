using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    //EnemyTrace
    private NavMeshAgent agent = null;
    private SearchComponent searchcomponent = null;
    public GameObject enemyTransform;

    [SerializeField] private GameObject target;

    //AttackTarget
    [SerializeField] private bool HitDamage; // 공격
    [SerializeField] private bool HitSpeed;  // 공격속도
    [SerializeField] private float EnemyStr;
    private float Hitrange = 100f;
    private bool isPlayer = false;
    private bool targetAttack;
    private bool isDead;
    public float health;
    Animator anim;


    private void OnEnable()
    {
        //EnemyTrace
        searchcomponent = GetComponent<SearchComponent>();
        agent = GetComponent<NavMeshAgent>();

        //AttackTarget
        anim = GetComponent<Animator>();
        targetAttack = false;
        isDead = false;

        Hitrange += ZombieManager.Instance.AddZombieHP;
        EnemyStr += ZombieManager.Instance.AddZombiePower;
        StartCoroutine(ZombieAttack());

    }
    private IEnumerator ZombieAttack()
    {
        while (true)
        {
            Queue<GameObject> queue = searchcomponent.SearchedObjs;
            int cnt = queue.Count;
            //Debug.Log(searchcomponent.SearchedObjs);
            //Debug.Log(searchcomponent);
            for (int ix = 0; ix < cnt; ix++)
            {
                try
                {
                    GameObject obj = queue.Dequeue(); //SearchComponent 에서 쌓인 큐를 비우는 것.
                    if (obj.tag == "Player")
                    {
                        isPlayer = true;
                        break;
                    }
                    else if (obj.tag == "Nexus")
                    {
                        isPlayer = false;
                    }
                }
                catch (Exception ex) { }
            }

            if (isPlayer) // 플레이어가 감지 되었다면 
            {
                target = ZombieManager.Instance.Player;
            }
            else
            {
                target = ZombieManager.Instance.Nexus;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f);

            health = GetComponent<ObjectStat>().HealthPoint;
            //GetComponent<ObjectStat>().TakeDamage(0.005f);
            if (health < 0)
            {
                this.agent.isStopped = true;
                GetComponent<Animator>().SetBool("isDead", true);
                StartCoroutine(destroy());

            }

            if (Vector3.Distance(transform.position, target.transform.position) < 5)
            {
                anim.SetBool("TargetFind", false);
                anim.SetBool("targetAttack", true);
                print("공격시작");

                isPlayer = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator destroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            ScoreManager.Instance.AddScore(10);
            ScoreManager.Instance.AddCoin(10);
            Destroy(gameObject);
            yield break;
        }
    }
    public void ZombieSetting()
    {
        StartCoroutine(ZombieAttack());
    }
}