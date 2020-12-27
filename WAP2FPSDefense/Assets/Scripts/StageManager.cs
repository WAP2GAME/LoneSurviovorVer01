using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoSingleton<StageManager>
{
    private float Count = 60;

    public int AddZombieHp;
    public int AddZombiePower;



    public GameObject Player;
    public GameObject Nexus;



    void Awake()
    {
        AddZombieHp = 0;
        AddZombiePower = 0;
    }

    void FixedUpdate()
    {
        if(Count < 0)
        {
            Count = 60;
            AddZombieHp += 100;
            AddZombiePower += 10;
        }
    }


    public void StageReset()
    {
        AddZombieHp = 0;
        AddZombiePower = 0;
    }
}
