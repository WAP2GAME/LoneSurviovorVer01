using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Security.Cryptography;
using UnityEngine;

public class Granade : Bullet
{
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private float explodeDelay = 1f;
    protected override void OnTriggerEnter(Collider col)
    {
        return;
    }

    private IEnumerator Explode()
    {
        yield return WaitTime.GetWaitForSecondOf(explodeDelay);
        var particleObj = Instantiate(particle, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        Destroy(particleObj, 1f);
        Destroy(gameObject, 1f);
        var enemies = Physics.SphereCastAll(transform.position, 30f, Vector3.one, 0.01f);
        foreach (var enemy in enemies)
            if (enemy.collider.gameObject.CompareTag("Target"))
                enemy.collider.GetComponent<ObjectStat>().TakeDamage(99999);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Explode());
    }
}
