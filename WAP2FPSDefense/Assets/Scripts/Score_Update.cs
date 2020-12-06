using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Update : MonoBehaviour
{
    Text scoreLabel;

    void Awake()
    {
        scoreLabel = GetComponent<Text>();
    }

    void Update()
    {
        scoreLabel.text = "Score: " + Score_Manager.score.ToString();
    }

}
