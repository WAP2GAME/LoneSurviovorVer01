using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{

    //적 캐릭터에게 필요한 컴포넌트들
    private Transform EnemyTr;
    private Transform PlayerTr;
    private NavMeshAgent nvAgent;
    private GameObject player;
    private Player playerScript;

    //적 캐릭터에게 필요한 변수들
    public enum EnemyState { idle, trace, attack, die, PlayerDie };
    public EnemyState enemyState = EnemyState.idle;
    public float attackDist = 2.0f;
    public float BornTime;

    public int EnemyNowHp = 10;

    public GameObject UIObj;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        UIObj = GameObject.Find("SPAWNPOINT");

        EnemyTr = this.gameObject.GetComponent<Transform>();
        PlayerTr = player.GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<Player>();

    }

    void OnEnable()
    {
        enemyState = EnemyState.idle;
        BornTime = Time.time + 0.5f;

        StartCoroutine(this.CheckEnemyState());
        StartCoroutine(this.EnemyAction());
    }

    /// <summary>
    /// 적 상태 확인 코루틴
    /// </summary>
    IEnumerator CheckEnemyState()
    {
        while (enemyState != EnemyState.die
            && enemyState != EnemyState.PlayerDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(PlayerTr.position, EnemyTr.position);

            if (BornTime <= Time.time && enemyState == EnemyState.idle)
            {
                enemyState = EnemyState.trace;
            }

            if (enemyState != EnemyState.idle)
            {

                if (playerScript.PlayerHP == 0)
                {
                    enemyState = EnemyState.PlayerDie;
                }
                else if (dist <= attackDist)
                {
                    enemyState = EnemyState.attack;
                }
                else if (EnemyNowHp <= 0)
                {
                    StopAllCoroutines();
                    enemyState = EnemyState.die;
                    StartCoroutine(EnemyAction());
                }
                else
                {
                    enemyState = EnemyState.trace;
                }

            }

            yield return null;

        }
    }

    IEnumerator EnemyAction()
    {
        while (enemyState != EnemyState.PlayerDie)
        {

            switch (enemyState)
            {

                case EnemyState.trace:
                    nvAgent.SetDestination(PlayerTr.position);
                    break;

                case EnemyState.attack:
                    nvAgent.ResetPath();
                    playerScript.TakeDamage();
                    Debug.Log("Enemy Attack Player!");
                    break;

                case EnemyState.die:
                    nvAgent.ResetPath();
                    UIObj.GetComponent<UICtrl>().ChangeScore();
                    UIObj.GetComponent<UICtrl>().ChangeCoin();
                    this.gameObject.SetActive(false);
                    break;
            }

            yield return null;

        }

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {

            Destroy(coll.gameObject);
            EnemyNowHp -= 10;

            Debug.Log("Enemy Hit!");
        }
    }
}