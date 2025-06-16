using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackState : StateMachineBehaviour
{
    Transform player;

    NavMeshAgent agent;

    public float attackingDistance = 2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (SoundManage.instance.zombieSource.isPlaying == false)
        {
            SoundManage.instance.zombieSource.PlayOneShot(SoundManage.instance.zombieAttack);
            

        }
        LookAtPlayer();

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > attackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction=player.position- agent.transform.position;
        agent.transform.rotation= Quaternion.LookRotation(direction);
        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation=Quaternion.Euler(0, yRotation, 0);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManage.instance.zombieSource.Stop();
    }

}
