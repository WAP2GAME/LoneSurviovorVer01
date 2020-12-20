using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private int score = 0;
    private int coin = 0;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text coinText;

    public int Score
    {
        get => score;
    }

    public int Coin
    {
        get => coin;
    }


    private void RefreshUI()
    {
        scoreText.text = "Score : "+score;
        coinText.text = "Coint : "+coin;
    }
    public void AddScore(int score)
    {
        if (score < 0)
            return;

        this.score += score;
        RefreshUI();
    }

    public bool AddCoin(int coin)
    {
        if (coin + this.coin >= 0)
        {
            this.coin += coin;
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsPurchasable(int coin)
    {
        return this.coin - coin >=0;
    }

    public void InitScore()
    {
        score = 0;
        RefreshUI();
    }
}
