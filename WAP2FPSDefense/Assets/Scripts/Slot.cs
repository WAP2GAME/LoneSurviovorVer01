using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public ItemBase item; // 획득한 아이템.
    public int itemCount; // 획득한 아이템의 개수.
    public Image itemIcon; // 아이템의 이미지.

    private Stack<ItemBase> items = new Stack<ItemBase>();

    // 필요한 컴포넌트.
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    public static int ItemCount { get; internal set; }


    // 이미지의 투명도 조절.
    private void SetColor(float _alpha)
    {
        Color color = itemIcon.color;
        color.a = _alpha;
        itemIcon.color = color;
    }

    // 아이템 획득
    public void AddItem(ItemBase _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemIcon.sprite = item.ItemIcon;

            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
 
        SetColor(1);
    }

    // 아이템 개수 조정.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemIcon.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }


    public int Bullet()
    {
        return Inventory.Instance.AmmoCnt;
    }

  

    internal int ToInt()
    {
        throw new NotImplementedException();
    }
}
