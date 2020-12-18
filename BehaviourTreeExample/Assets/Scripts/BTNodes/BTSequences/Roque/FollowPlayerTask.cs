using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerTask : BTNode
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Transform playerTransform;

    public FollowPlayerTask(Transform _playerTransform, Animator _anim, NavMeshAgent _navMeshAgent)
    {
        playerTransform = _playerTransform;
        anim = _anim;
        navMeshAgent = _navMeshAgent;
    }

    public override BTNodeStates Evaluate()
    { 
        if(Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) > 4f)
        {
            Debug.Log("Player Out of range, lets walk");

            anim.SetTrigger("isWalking");

            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(playerTransform.position);
            return BTNodeStates.SUCCES;
        }
        else if(Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) <= 4f && Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) >= 2f)
        {
            Debug.Log("We are close enough");

            anim.SetTrigger("isIdle");
            navMeshAgent.isStopped = true;
            return BTNodeStates.SUCCES;
        }
        else
        {
            Debug.Log("FollowPlayerTask::FAILURE");

            return BTNodeStates.FAILURE;
        }
    }
}