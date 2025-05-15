using UnityEngine;

public class EnemyDamage : Damage
{
    [Header("EnemyDamage")]

    protected EnemyCrlt enemyCrlt;

    private void Awake()
    {
        this.enemyCrlt = GetComponent<EnemyCrlt>();
    }

    protected override void ColiderSendDamge(Collider2D collider)
    {
        base.ColiderSendDamge(collider);
        this.enemyCrlt.hp.Receive(1);
        
    }
}
