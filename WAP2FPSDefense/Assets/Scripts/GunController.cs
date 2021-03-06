﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Security.Cryptography;
using UnityEngine;

public class GunController : MonoBehaviour
{

    // 활성화 여부.
    public static bool isActivate = false;

    // 현재 장착된 총
    public Gun currentGun;


    // 연사 속도 계산
    private float currentFireRate;



    // 상태 변수
    private bool isReload = false;
    [HideInInspector]
    public bool isFineSightMode = false;


    // 본래 포지션 값.
    private Vector3 originPos;


    // 효과음 재생
    private AudioSource audioSource;


    // 레이저 충돌 정보 받아옴.
    private RaycastHit hitInfo;


    // 필요한 컴포넌트
    [SerializeField]
    private Camera theCam;
    [SerializeField]
    private Crosshair theCrosshair;



    // 피격 이펙트.
    [SerializeField]
    private GameObject hit_effect_prefab;
    public GameObject bulletPrefab;

    ObjectPool objectPool;
    
    GameObject bullet;




    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        theCrosshair = FindObjectOfType<Crosshair>();

        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        objectPool = ObjectPool.Instance;

        if (!objectPool.IsContainObject(bulletPrefab.name))
            objectPool.AddObject(bulletPrefab, bulletPrefab.name, 100);

    }

    private void OnEnable()
    {
        currentGun.transform.position = originPos;
        currentGun.anim.Play("default");
        isReload = false; 
    }

    void Update()
    {
        if (isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
        }

    }


    // 연사속도 재계산
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
            Fire();
    }

    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
        }
    }



    // 발사 후 계산.
    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; // 연사 속도 재계산.
        PlaySE(currentGun.fire_Sound);

        bullet = objectPool.GetPooledObject(bulletPrefab.name);
        if(bullet != null)
        {
            bullet.transform.position = transform.position+transform.forward;
            bullet.transform.LookAt(transform.position + transform.forward * 1.2f);
            bullet.SetActive(true);
        }
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
            StartCoroutine(ReloadCoroutine());
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    // 재장전
    private IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;

            currentGun.anim.SetTrigger("DoReload");


            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return WaitTime.GetWaitForSecondOf(currentGun.reloadTime);

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }


            isReload = false;
        }

    }

    // 반동 코루틴
    private IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);


        currentGun.transform.localPosition = originPos;

        // 반동 시작
        while (currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
            yield return null;
        }

        // 원위치
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
            yield return null;
        }

    }


    // 사운드 재생.
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }


    public Gun GetGun()
    {
        return currentGun;
    }

    public bool GetFineSightMode()
    {
        return isFineSightMode;
    }

    public void GunChange(Gun _gun)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate = true;
    }
}

