using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance;
    public  HP hp;
    public PlayerStatus playerStatus;
    void Awake()
    {
        PlayerCtrl.Instance = this;
        hp = GetComponent<HP>();
        playerStatus=GetComponent<PlayerStatus>();
    }
}
