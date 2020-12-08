using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    // 레이저 충돌 정보 받아옴.
    private RaycastHit hitInfo;


    float speed = 100f;
    float time;
    public int Damage = 20;

    [SerializeField]
    private GameObject Cam;

    public Vector3 direction;

    public void OnEnable()
    {
        //타임 변수 초기화
        time = Time.time;
        Cam = GameObject.Find("Main Camera");
       GetComponent<Rigidbody>().AddForce(Cam.transform.forward * speed);
    }


    // Update is called once per frame
    void Update()
    {
        //transform.forward = 이 오브젝트의 정면 벡터 값
        //translate == 현재 오브젝트의 포지션에서 인자로 주어진 벡터 값을 더함


        if (Time.time > time + 1 && gameObject.activeSelf)
            gameObject.SetActive(false);

      
    }



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Target")
        {
            col.gameObject.SetActive(true);
            col.GetComponent<ObjectStat>().TakeDamage(99999);
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

  
    }
}
