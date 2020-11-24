using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{

    public Text txtScore;
    public Text txtLife;
    public Text txtBestScore;
    public Text txtCoin;

    private int totScore = 0;
    private int totCoin = 0;

    // Use this for initialization
    void Start()
    {

        txtScore.text = "<color=#00ff00ff>Score</color> <color=#ff0000>0</color>";
        txtBestScore.text = "<color=#00ff00ff>BestScore</color> <color=#ff0000>" +
            PlayerPrefs.GetInt("BestScore", 0).ToString() + "</color>";
        txtLife.text = "<color=#00ff00ff>Life</color> <color=#ff0000>50</color>";
        txtCoin.text = "<color=#00ff00ff>Score</color> <color=#ff0000>0</color>";
    }

    public void ChangeScore()
    {
        totScore += 10;
        txtScore.text = "<color=#00ff00ff>Score</color> <color=#ff0000>" +
            totScore.ToString() + "</color>";

        if (totScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", totScore);

            txtBestScore.text = "<color=#00ff00ff>BestScore</color> <color=#ff0000>" +
                totScore.ToString() + "</color>";
        }
    }

    public void ChangeCoin()
    {
        totCoin += 10;
        txtCoin.text = "<color=#00ff00ff>Score</color> <color=#ff0000>" +
            totCoin.ToString() + "</color>";
    }

    public void ChangeHp(int PlayerHp)
    {
        int totLife = PlayerHp;

        txtLife.text = "<color=#00ff00ff>Life</color> <color=#ff0000>" +
            totLife.ToString() + "</color>";
    }
}