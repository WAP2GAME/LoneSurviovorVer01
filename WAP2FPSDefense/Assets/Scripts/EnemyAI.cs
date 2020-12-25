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

    [SerializeField] public GameObject target;

    //AttackTarget
    [SerializeField] private float EnemyStr;
    [SerializeField] private float hitRange = 1f;
    [SerializeField] private float moveSpeed = 0.1f;
    private bool isPlayer = false;
    public float health;
    private Animator anim;

    private void OnEnable()
    {
        //EnemyTrace
        searchcomponent = GetComponent<SearchComponent>();
        agent = GetComponent<NavMeshAgent>();

        //AttackTarget
        anim = GetComponent<Animator>();
        StartCoroutine(ZombieAttack());
    }

    private IEnumerator ZombieAttack()
    {
        while (true)
        {
            Queue<GameObject> queue = searchcomponent.SearchedObjs;
            int cnt = queue.Count;
            for (int ix = 0; ix < cnt; ix++)
            {
                GameObject obj = queue.Dequeue(); //SearchComponent 에서 쌓인 큐를 비우는 것.
                if (obj.tag == "Player")
                {
                    isPlayer = true;
                    break;
                }
                else if (obj.name == "Nexus")
                    isPlayer = false;
            }

            if (isPlayer) // 플레이어가 감지 되었다면 
                target = ZombieManager.Instance.Player;
            else
                target = ZombieManager.Instance.Nexus;

            transform.LookAt(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < hitRange)
            {
                anim.SetBool("TargetFind", false);
                anim.SetBool("targetAttack", true);
                target.GetComponent<ObjectStat>().TakeDamage(1f);
                yield return new WaitForSeconds(1f);
                continue;
            }
            else
            {
                anim.SetBool("TargetFind", true);
                anim.SetBool("targetAttack", false);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator destroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

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