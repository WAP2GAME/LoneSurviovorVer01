using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EBodyParts
{
    Head,
    Leg,
    Arm,
    Body
}
class EnemyStat : ObjectStat
{
    [SerializeField]
    private Stat overallStat;
    [SerializeField]
    private Dictionary<EBodyParts, Stat> bodyPartStats = new Dictionary<EBodyParts, Stat>();

    public override void TakeDamage(float damage, EBodyParts hitParts)
    {
        base.TakeDamage(damage);
        Stat hitPart = bodyPartStats[hitParts];
        hitPart.TakeDamage(damage);

        AffectBodyInjury(hitParts);
    }

    private void AffectBodyInjury(EBodyParts part)
    {

    }
}

