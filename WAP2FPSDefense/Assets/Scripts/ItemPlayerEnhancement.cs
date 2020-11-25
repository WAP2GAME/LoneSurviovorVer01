using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPlayerEnhancement : ItemBase
{
    protected GameObject player;
    public bool HasActivated
    {
        private set;
        get;
    }

    public abstract void EnhancePlayer();
    protected void FindPlayerComponents()
    {

    }
}
