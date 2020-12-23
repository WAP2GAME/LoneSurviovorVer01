using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    private Slot[] slots = new Slot[50];
    public List<Slot> ItemConsumables
    {
        get
        {
            List<Slot> items = new List<Slot>(10);
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] != null && slots[i].Item != null && slots[i].Item is ItemConsumable)
                    items.Add(slots[i]);
            return items;
        }
    }
    public bool AddItem(ItemBase item)
    {
        int idx = GetIdleSlotIndex(item);
        if (idx == -1)
            return false;
        slots[idx].AddItem(item);
        return true;
    }

    public void AddItemCount(ItemBase item , int cnt)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null && slots[i].Item == item)
                slots[i].IncreaseItem(cnt);
    }

    private int GetIdleSlotIndex(ItemBase item)
    {
        int nullSlot = -1;
        for (int i = 0; i < slots.Length; i++)
            if (slots[i].Item.Equals(item))
                return i;
            else if (slots[i].Item == null)
                nullSlot = i;
        return nullSlot;
    }
    public Slot GetSlot(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null && slots[i].Item.Equals(itemName))
                return slots[i];
        return null;
    }

    public Slot GetSlot(ItemBase item)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null && slots[i].Item.Equals(item))
                return slots[i];
        return null;
    }
}

public class Slot
{
    private ItemBase item;
    private int itemCount; 
    
    public bool IsOcuupied
    {
        get => item != null;
    }
    public ItemBase Item
    {
        get => item;
    }

    public int ItemCount
    {
        get => ItemCount;
    }
   
    public bool Use()
    {
        if (!(item is ItemConsumable))
            return false;
        var itemConsume = item as ItemConsumable;
            DecreaseItem(1);
        return true;
    }

    public bool AddItem(ItemBase item)
    {
        if (!IsAbleToPut(item))
            return false;
        this.item = item;
        itemCount = 1;
        return true;
    }

    private bool IsAbleToPut(ItemBase item)
    {
        return this.item == null && item != null;
    }

    public bool IncreaseItem(int cnt)
    {
        if (itemCount + cnt <= item.StockCountLimit)
        {
            itemCount += cnt;
            return true;
        }
        else
            return false;
    }

    public void DecreaseItem(int cnt)
    {
        if (item == null)
            return;
        if (itemCount <= cnt)
            ClearSlot();
        else
            itemCount -= cnt;
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
    }
}
