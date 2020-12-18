using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSpottedTask : BTNode
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Transform playerTransform;
    private FieldOfView fov;

    private Transform closesTransform = null;  
    private List<Transform> hidingSpots = new List<Transform>();
    private float minDist = Mathf.Infinity;


    public PlayerSpottedTask(List<Transform> _hidingSpots, FieldOfView _fov, Transform _playerTransform, Animator _anim, NavMeshAgent _navMeshAgent)
    {
        hidingSpots = _hidingSpots;
        fov = _fov;
        playerTransform = _playerTransform;
        anim = _anim;
        navMeshAgent = _navMeshAgent;
    }

    public override BTNodeStates Evaluate()
    {
        if(fov.IsHumanoidVisible == true)
        {
            Debug.Log("Player has been spotted, Have to hide");
            foreach (Transform hidingSpot in hidingSpots)
            {
                float _dist = Vector3.Distance(navMeshAgent.transform.position, hidingSpot.position);
                if(_dist < minDist)
                {
                    closesTransform = hidingSpot;
                    minDist = _dist;
                }
            }
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(closesTransform.position);

            if(Vector3.Distance(navMeshAgent.transform.position, closesTransform.position) <= 1f)
            {
                anim.SetTrigger("isIdle");
                navMeshAgent.isStopped = true;
            }

            return BTNodeStates.SUCCES;
        }
        else
        {
            Debug.Log("PlayerSpottedTask::FAILURE");

            return BTNodeStates.FAILURE;
        }
    }
}