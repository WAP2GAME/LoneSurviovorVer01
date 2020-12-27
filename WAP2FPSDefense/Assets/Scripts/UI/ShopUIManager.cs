using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUIManager : MonoBehaviour ,IStageEndObserver
{
    [SerializeField]
    private Text textCoin;
    [SerializeField]
    private Button[] itemSlotsBtn;
    [SerializeField]
    private List<ItemStock> items = new List<ItemStock>();
    [SerializeField]
    private Button btnExit;

    private ShopItemGenerator itemGenerator = new ShopItemGenerator();

    public void EndStage()
    {
        items.Clear();
        InitItemStockBtns();
        items = itemGenerator.RandomConsumableItems;
        AllocItemInfoToButton();
    }

    private void InitItemStockBtns()
    {
        for (int i = 0; i < itemSlotsBtn.Length; i++)
        {
            itemSlotsBtn[i].image.sprite = null;
            itemSlotsBtn[i].GetComponentInChildren<Text>().text = null;
        }
    }

    private void AllocItemInfoToButton()
    {
        for (int i = 0; i < itemSlotsBtn.Length; i++)
        {
            if (i >= items.Count)
                break;

            itemSlotsBtn[i].image.sprite = items[i].item.ItemIcon;
            itemSlotsBtn[i].GetComponentInChildren<Text>().text = items[i].cnt.ToString();
        }
        textCoin.text = "You Have "+ScoreManager.Instance.Coin.ToString()+"Won. ";
    }

    private void OnClick()
    {
        var clickedBtn = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < itemSlotsBtn.Length; i++)
            if (itemSlotsBtn[i].gameObject == clickedBtn)
                Purchase(i);
    }

    private void Purchase(int idx)
    {
        if (idx >= items.Count || items[idx].cnt <= 0)
            return;

        var cost = items[idx].item.Cost;
        if (ScoreManager.Instance.IsPurchasable(cost))
        {
            ScoreManager.Instance.AddCoin(-cost);
            var item = items[idx];
            item.cnt -= 1;
            items[idx] = item;

            Inventory.Instance.AddItem(item.item);
        }
        AllocItemInfoToButton();
    }

    private void Awake()
    {
        btnExit.onClick.AddListener(() => { gameObject.SetActive(false); });
        for (int i = 0; i < itemSlotsBtn.Length; i++)
            itemSlotsBtn[i].onClick.AddListener(OnClick);
    }
}
public struct ItemStock
{
    public ItemBase item;
    public int cnt;
}

class ShopItemGenerator
{
    [SerializeField]
    private int[] ammoCntRange = { 50, 150 };
    [SerializeField]
    private int[] potionKindCntRange = { 1, 3 };
    [SerializeField]
    private int[] potionCntRange = { 15, 25};

    public List<ItemStock> RandomConsumableItems
    {
        get
        {
            int itemKindCnt = UnityEngine.Random.Range(potionKindCntRange[0], potionKindCntRange[1] + 1);
            List<ItemStock> items = new List<ItemStock>(3);
            var itemConsumes = ItemContainer.Instance.ItemConsumables;
            for (int i = 0; i < itemKindCnt; i++)
            {
                ItemStock itemStock;
                itemStock.item = itemConsumes[UnityEngine.Random.Range(0, itemConsumes.Count)];
                itemStock.cnt = UnityEngine.Random.Range(potionCntRange[0], potionCntRange[1] + 1);
                items.Add(itemStock);
            }

            return items;
       }
    }
}
