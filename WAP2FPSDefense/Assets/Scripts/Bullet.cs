using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    // 레이저 충돌 정보 받아옴.
    private RaycastHit hitInfo;


    private float speed = 1000f;
    private float time;
    private int Damage = 20;

    [SerializeField]
    private GameObject Cam;

    public Vector3 direction;

    public void OnEnable()
    {
        //타임 변수 초기화
        time = Time.time;
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(Cam.transform.forward * speed);
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + 2 && gameObject.activeSelf)
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
