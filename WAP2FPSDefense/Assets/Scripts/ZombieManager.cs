using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZombieManager : MonoSingleton<ZombieManager>
{ 
    //private ZombieManager zombieManger;
    public GameObject Player;
    public GameObject Nexus;
    public int AddZombieHP;
    public int AddZombiePower;
    float ZombieCount;
    IEnumerator zombieEhance()
    {
        ZombieCount += Time.deltaTime;
        if (ZombieCount > 10)
        {
            ZombieCount = 0;
            AddZombieHP += 100;
            AddZombiePower += 10;
            ZombieCount += Time.deltaTime;
        }
        yield return new WaitForSecondsRealtime(10);
    }
    public void FixedUpdate()
    {
        StartCoroutine(zombieEhance());
    }
    public void ZombieReset()
    {
        AddZombieHP = 0;
        AddZombiePower = 0;
    }
}