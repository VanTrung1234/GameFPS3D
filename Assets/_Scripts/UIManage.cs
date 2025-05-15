using UnityEngine;

public class UIManage : MonoBehaviour
{
    public static UIManage Instance;
    public GameObject bnGameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UIManage.Instance = this;
        this.bnGameOver = GameObject.Find("bnGameOver");
        this.bnGameOver.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
