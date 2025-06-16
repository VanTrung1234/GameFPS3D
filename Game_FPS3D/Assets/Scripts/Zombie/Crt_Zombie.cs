using UnityEngine;

public class Crt_Zombie : MonoBehaviour
{


    [SerializeField] private ZombieHand hand;
    public int zombieDamage;

    private void Awake()
    {
       
        
            hand = GetComponentInChildren<ZombieHand>();
        
    }
    private void Start()
    {
        hand.damage = zombieDamage;
    }
}
