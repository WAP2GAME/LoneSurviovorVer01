using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EItemType
{
    ammo,
    consumable,
    playerEnhancement
}

[SerializeField]
public class ItemBase : ScriptableObject
{
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected EItemType itemType;
    [SerializeField]
    protected string describtion;
    [SerializeField]
    protected int cost;
    [SerializeField]
    protected Sprite itemIcon;

    public string ItemName
    {
        get => itemName;
    }
    public string Describtion
    {
        get => describtion;
    }
    public int Cost
    {
        get => cost;
    }
    public Sprite ItemIcon
    {
        get => itemIcon;
    }
    public EItemType ItemType
    {
        get => itemType;
    }
}
