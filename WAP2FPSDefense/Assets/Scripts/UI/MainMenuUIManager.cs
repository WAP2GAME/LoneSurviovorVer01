using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class MainMenuUIManager : MonoBehaviour
 {
    [SerializeField]
    private Button startBtn;
    public void OnClick()
    {
        GameStageManger.Instance.MoveStage();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (startBtn)
            startBtn.onClick.AddListener(OnClick);
    }
}

