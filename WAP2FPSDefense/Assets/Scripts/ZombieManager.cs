using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZombieManager :MonoBehaviour
{ 
    //private ZombieManager zombieManger;
    private static ZombieManager Instance = null;
    public GameObject Player;
    public GameObject Nexus;
    public static ZombieManager instance
    {
        get
        {
            return Instance;
        }
    }
    private void Awake()
    {
        //zombieManger = ZombieManager.Instance;
        if ( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        ZombieCount = GetComponent<StageTimeCounter>().Count;
    }
    public int AddZombieHP;
    public int AddZombiePower;
    float ZombieCount;
    IEnumerator zombieEhance()
    {
        ZombieCount += Time.deltaTime;
        if (ZombieCount > 10)
        {
            ZombieCount = 0;
            Debug.Log("zombie Enhance");
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