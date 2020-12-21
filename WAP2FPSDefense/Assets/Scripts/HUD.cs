using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{


    // 필요한 컴포넌트
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;

    // 필요하면 HUD 호출, 필요 없으면 HUD 비활성화.
    [SerializeField]
    private GameObject go_BulletHUD;


    // 총알 개수 텍스트에 반영
    [SerializeField]
    private Text[] text_Bullet;

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_Bullet[0].text = theGunController.CurrentBullet.ToString();
        text_Bullet[1].text = theGunController.reloadBulletCount.ToString();
        text_Bullet[2].text = theGunController.currentBulletCount.ToString();
    }

}
