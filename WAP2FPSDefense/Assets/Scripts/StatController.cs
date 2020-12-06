using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{

    [SerializeField]
    private ObjectStat theObjectStat;

    [SerializeField]
    private Image[] imagestat;

    private Stat stat;
   

    void Update()
    {
        CheckStat();
    }

    private void CheckStat()
    {
        float Healthrate = theObjectStat.HealthPoint / theObjectStat.HealthPointMax;
        imagestat[0].fillAmount = Healthrate;
    }

    void OnEnable()
    {
        theObjectStat = GetComponent<ObjectStat>();
    }
}
