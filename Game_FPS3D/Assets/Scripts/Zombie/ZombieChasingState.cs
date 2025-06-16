using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChasingState : StateMachineBehaviour
{
    Transform player;

    NavMeshAgent agent;

    public float stopChasingDistance = 21f;
    public float spped = 6f;
    public float attackingDistance = 2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = spped;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SoundManage.instance.zombieSource.isPlaying == false)
        {
            SoundManage.instance.zombieSource.PlayOneShot(SoundManage.instance.zombieChase);

        }
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);
        float distanceFromPlayer= Vector3.Distance(player.position,animator.transform.position);

        if (distanceFromPlayer < attackingDistance )
        {
            animator.SetBool("isAttacking", true);
        }
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        agent.SetDestination(animator.transform.position);
        SoundManage.instance.zombieSource.Stop();
    }
}
