using UnityEngine;

public class PlayerHP : HP
{
    PlayerCtrl playerCtrl;
    
    void Awake()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }
    public override void Receive(int damage)
    {
        base.Receive(damage);
        if (this.checkDead()) 
        {

            this.playerCtrl.playerStatus.Dead();
            UIManage.Instance.bnGameOver.SetActive(true);
        }
    }
}
