using UnityEngine;

public class BomdDamage : HP
{
    private void Reset()
    {
        this.hp = 1;
    }
    
    public override void Receive(int damage)
    {
        base.Receive(damage);
        if (this.checkDead())
        {
            
            Destroy(gameObject);
            EffectManage.Instance.SpawnVFX("Explosion_B", transform.position, transform.rotation);
        }
    }
}
