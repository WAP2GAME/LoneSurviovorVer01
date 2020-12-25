using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField]
    private ObjectStat targetStat;
    private Image healthBar;

    private void Update()
    {
        var ratio = targetStat.HealthPoint / targetStat.HealthPointMax;
        healthBar.fillAmount = ratio;
    }
    private void Awake()
    {
        healthBar = GetComponent<Image>();
    }
}
