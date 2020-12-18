using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PatrolTask : BTNode
{
    private NavMeshAgent navMeshAgent;
    private Transform[] waypointTargets;
    private FieldOfView fow;
    private Animator anim;
    private Transform target;
    private ChangeTextState changeTextState;

    private float elapseTime = 2f;
    private float maxElapseTime = 2f;
    private float minDistanceToTarget = 1.5f;

    public PatrolTask(FieldOfView _fow, Animator _anim, Transform[] _waypointTargets, NavMeshAgent _navMeshAgent, ChangeTextState _changeTextState)
    {
        fow = _fow;
        anim = _anim;
        waypointTargets = _waypointTargets;
        navMeshAgent = _navMeshAgent;
        changeTextState = _changeTextState;
    }

    public override BTNodeStates Evaluate()
    {
        if(fow.IsHumanoidVisible == false)
        {
            changeTextState.TextStringGuard = "PatrolTask::Patrolling";
            anim.SetTrigger("isWalking");

            if (!target)
            {
                target = waypointTargets[UnityEngine.Random.Range(0, waypointTargets.Length)];
                navMeshAgent.SetDestination(target.position);
            }

            float _distToTarget = Vector3.Distance(navMeshAgent.transform.position, target.position);

            if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                Debug.LogWarning(navMeshAgent.name + " Can't find a path to " + target.name);
                return BTNodeStates.FAILURE;
            }

            if (_distToTarget <= minDistanceToTarget)
            {
                changeTextState.TextStringGuard = "PatrolTask::StandingWatch";

                Debug.Log(navMeshAgent.name + " reached " + target.name);

                anim.ResetTrigger("isWalking");
                navMeshAgent.speed = 0;
                anim.SetTrigger("isIdle");
                elapseTime -= Time.deltaTime;
                
                if (elapseTime <= 0f)
                {
                    anim.ResetTrigger("isIdle");

                    navMeshAgent.speed = 2.5f;

                    anim.SetTrigger("isWalking");

                    target = waypointTargets[UnityEngine.Random.Range(0, waypointTargets.Length)];
                    navMeshAgent.SetDestination(target.position);
                    elapseTime = maxElapseTime;
                }
                return BTNodeStates.SUCCES;
               
            }
            else if (target)
            {
                navMeshAgent.SetDestination(target.position);
                Debug.Log("Patrolling...");
                return BTNodeStates.SUCCES;
            }
            else
            {
                return BTNodeStates.FAILURE;
            }
        }
        else if(fow.IsHumanoidVisible == true)
        {
            return BTNodeStates.SUCCES;
        }
        else
        {
            Debug.LogWarning("Stop Patrolling");
            return BTNodeStates.FAILURE;
        }
    }
}