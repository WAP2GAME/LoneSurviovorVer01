using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionHeal", menuName = "Scriptable Object/Potion Heal", order = 1)]
public class ItemPotion : ItemConsumable
{
    [SerializeField]
    private int heal = 50;
    [SerializeField]
    private GameObject particle;
    public override void Use()
    {
        ObjectStat stat;
        if (particle != null)
            Destroy(Instantiate(particle, GameStageManger.Instance.Player.transform.position + GameStageManger.Instance.Player.transform.forward, Quaternion.identity), 1);
        if (GameStageManger.Instance.Player != null && (stat = GameStageManger.Instance.Player.GetComponent<ObjectStat>()) != null)
            stat.TakeHeal(heal);
    }
}
