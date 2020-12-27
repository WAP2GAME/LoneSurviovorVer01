using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Granade", menuName = "Scriptable Object/Granade", order = 1)]
public class ItemGranade : ItemConsumable
{
    [SerializeField]
    private int damage = 99900;
    [SerializeField]
    private float radius = 20f;
    [SerializeField]
    private GameObject granadePrefab;
    public override void Use()
    {
        if (granadePrefab != null)
            Instantiate(granadePrefab, GameStageManger.Instance.Player.transform.position + GameStageManger.Instance.Player.transform.forward, Quaternion.identity);
    }
}