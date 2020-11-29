using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{

    public float moveSpeed = 7f;     //플레이어의 속도
    PlayerMovement Controller;      //플레이어 움직임 구현 스크립트
    GunController PlayerGunCtrl;

    public Camera viewCamera;               //플레이어 관찰 카메라
    Ray ray;                        //게이머 마우스 위치 
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);    //스테이지 바닥
    float rayDistance;                                          //카메라 - 마우스 - 바닥 까지의 거리

    public int PlayerHP = 50;
    private float PlayerInvin;
    private MeshRenderer mesh;

    public GameObject UIObj;

    // Use this for initialization
    void Start()
    {
        UIObj = GameObject.Find("SPAWNPOINT");

        Controller = GetComponent<PlayerMovement>();
        PlayerGunCtrl = GetComponent<GunController>();
        mesh = GetComponent<MeshRenderer>();

        PlayerInvin = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 움직임 구현(입력값 받음)
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        Controller.Move(moveVelocity);

        //플레이어 회전 구현
        ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            Controller.LookAt(point);
        }
        //총 발사 입력
        if (Input.GetMouseButton(0))
        {
            PlayerGunCtrl.Shoot();
        }
        //총 재장전 입력
        if (Input.GetKey(KeyCode.R)
            && PlayerGunCtrl.newGun.BulletNow != PlayerGunCtrl.newGun.BulletMax)
        {
            PlayerGunCtrl.Reload();
        }

        if (PlayerHP == 0)
        {
            this.gameObject.SetActive(false);

            SceneManager.LoadScene("GamePlay");
        }
    }


    public void TakeDamage()
    {
        if (Time.time > PlayerInvin)
        {
            PlayerHP -= 10;
            PlayerInvin = Time.time + 1.0f;

            Debug.Log("Player is Damaged! PlayerHp : " + PlayerHP);
            UIObj.GetComponent<UICtrl>().ChangeHp(PlayerHP);


            StartCoroutine(PlayerInvinVisible(PlayerInvin));
        }
    }

    IEnumerator PlayerInvinVisible(float P_InvinTime)
    {
        while (Time.time <= P_InvinTime)
        {
            this.mesh.material.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(0.1f);

            this.mesh.material.color = new Color(1f, 0f, 0f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}