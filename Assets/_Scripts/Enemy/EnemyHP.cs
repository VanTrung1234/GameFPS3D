using UnityEngine;

public class EnemyHP : HP
{
    protected EnemyCrlt enemyCrlt;

    private void Awake()
    {
        this.enemyCrlt = GetComponent<EnemyCrlt>();
    }

    private void Reset()
    {
        this.hp = 4;
    }

    public override void Receive(int damage)
    {
        base.Receive(damage);
        if (this.checkDead())
        {
            EffectManage.Instance.SpawnVFX("Explosion_A", transform.position, transform.rotation);
            this.enemyCrlt.despawned.Despawn();

        }
    }
}
