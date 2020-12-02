using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static int score;
    public static int coin;

    private void Awake()
    {
        score = 0;
        coin = 0;
    }
}
