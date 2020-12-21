using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 class MainMenuUIManager : MonoBehaviour
 {
    public void OnClick()
    {
        GameStageManger.Instance.MoveStage();
    }
 }

