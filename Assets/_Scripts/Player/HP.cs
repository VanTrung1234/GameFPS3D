using UnityEngine;

public class HP : MonoBehaviour
{
    public int  hp=3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool checkDead()
    {
        return hp <= 0;
    }

    public virtual void Receive(int damage)
    {
        this.hp-= damage; 
    }
}
