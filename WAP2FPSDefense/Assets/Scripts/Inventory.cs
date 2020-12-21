using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{

    public static bool inventoryActivated = false;

    public int AmmoCnt
    {
        get;
        set;
    }

    public int GetNumberofItem(string ItemName)
    {
        for(int i = 0;  i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].item.ItemName == ItemName)
            { 
                return slots[i].itemCount;
            }
        }
        return -1;
    }

    public Slot GetSlot(string ItemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].item.ItemName == ItemName)
            {
                return slots[i];
            }
        }
        return null;
    }


    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    // 슬롯들.
    public Slot[] slots = new Slot[50];

    public int carryBulletCount; // 현재 소유하고 있는 총알 개수.

    // Use this for initialization
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(ItemBase _item, int _count = 1)
    {

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.ItemName == _item.ItemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }


        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
