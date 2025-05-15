using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("DamageSender")]
    public int dmg = 1;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ColiderSendDamge(collision);
        
    }

    protected virtual void ColiderSendDamge(Collider2D collision)
    {
        HP hp = collision.GetComponent<HP>();
        if (hp == null) return;

        hp.Receive(this.dmg);
    }
}
