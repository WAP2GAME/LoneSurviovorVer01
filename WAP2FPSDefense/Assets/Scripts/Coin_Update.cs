using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Update : MonoBehaviour
{
    Text coinLabel;

    void Awake()
    {
        coinLabel = GetComponent<Text>();
    }

    void Update()
    {
        coinLabel.text = "Coin: " + Score_Manager.score.ToString();
    }
}
