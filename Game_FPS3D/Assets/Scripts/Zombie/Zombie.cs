using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private int hp = 100;
    private Animator animator;

    private NavMeshAgent navMeshAgent;

    public bool isDead=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        
    }
    // Update is called once per frame
    public void takeDamage(int damage)
    {
        hp -= damage;

        if (hp > 0) { animator.SetTrigger("DAMAGE");
            SoundManage.instance.zombieSource1.PlayOneShot(SoundManage.instance.zombieHurt);

        }
        else
        {
            animator.SetTrigger("DIE");
            isDead = true;
            SoundManage.instance.zombieSource1.PlayOneShot(SoundManage.instance.zombieDead);
            //Destroy(gameObject);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3.5f); //attacking

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 21f); //Chasing

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere   (transform.position, 15f); //WalkingArea
    }
}
