using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image[] imgItem;
    [SerializeField]
    private Text[] textItemCnt;
    [SerializeField]
    private List<Slot> slots;

    private void AllocateConsumeItemsToSlot()
    {
        slots = Inventory.Instance.ItemConsumables;
        int i = 0;
        foreach (var item in slots)
        {
            if (item.Item == null)
                continue;

            imgItem[i].sprite = item.Item.ItemIcon;
            textItemCnt[i].text = item.ItemCount.ToString();
            if (++i >= imgItem.Length)
                break;
        }
        for (; i < imgItem.Length; i++)
        {
            imgItem[i].sprite = null;
            textItemCnt[i].text = null;
        }
    }

    private void UseItem()
    {
        int idx = 9909999;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            idx = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            idx = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            idx = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            idx = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            idx = 4;

        if (idx >= slots.Count)
            return;
        slots[idx].Use();
        AllocateConsumeItemsToSlot();
    }

    public void Update()
    {
        UseItem();
    }

    private void OnEnable()
    {
        AllocateConsumeItemsToSlot();
    }
}
