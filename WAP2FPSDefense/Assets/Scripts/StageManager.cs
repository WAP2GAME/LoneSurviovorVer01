using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private static StageManager Instance = null; 
    public static StageManager instance
    {
        get
        {
            return Instance;
        }
    }



    [SerializeField] Text CountText;
    float Count = 60;

    public int AddZombieHp;
    public int AddZombiePower;



    public GameObject Player;
    public GameObject Nexus;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        AddZombieHp = 0;
        AddZombiePower = 0;
    }

    void FixedUpdate()
    {
        Count -= Time.deltaTime;
        CountText.text = (int)Count + "";

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
