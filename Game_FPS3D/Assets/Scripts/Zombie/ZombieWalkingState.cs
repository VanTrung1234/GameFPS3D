using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieWalkingState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 10f;
    Transform player;

    NavMeshAgent agent;

    public float detectionAreaRadius = 18f;
    public float spped = 2f;

    List<Transform> waypointList=new List<Transform>();
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent=animator.GetComponent<NavMeshAgent>();
        agent.speed=spped;

        GameObject waypoint = GameObject.FindGameObjectWithTag("Wayponits");
        Debug.Log(waypoint.name);
        foreach (Transform t in waypoint.transform)
        {
            
            waypointList.Add(t);
        }
       
        Vector3 nextPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        
        agent.SetDestination(nextPosition);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (SoundManage.instance.zombieSource.isPlaying == false)
        {
            SoundManage.instance.zombieSource.PlayOneShot(SoundManage.instance.zombieWalking);

        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].position);
        }
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isWalking", false);
        }
        float distancePlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distancePlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        SoundManage.instance.zombieSource.Stop();
    }
}
