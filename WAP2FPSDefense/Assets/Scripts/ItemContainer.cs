using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoSingleton<ItemContainer>
{
    [SerializeField]
    private List<ItemBase> items = new List<ItemBase>(50);

    public List<ItemBase> Items
    {
        get
        {
            return new List<ItemBase>(items);
        }
    }

    public List<ItemConsumable> ItemConsumables
    {
        get
        {
            List<ItemConsumable> consumables = new List<ItemConsumable>();
            foreach(var a in items)
                if (a.ItemType == EItemType.consumable && a is ItemConsumable)
                    consumables.Add(a as ItemConsumable);

            return consumables;
        }
    }

}

