using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class ItemBase : ScriptableObject
{
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected string describtion;
    [SerializeField]
    protected float cost;
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
    public float Cost
    {
        get => cost;
    }
    public Sprite ItemIcon
    {
        get => itemIcon;
    }
}
