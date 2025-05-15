using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    PlayerCtrl playerCtrl;

    void Awake()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }
    void Update()
    {
      
    }

    public void Dead()
    {
        Debug.Log("trung");
    }
}
