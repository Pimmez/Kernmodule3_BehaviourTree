using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTask : BTNode
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private WeaponRecovered weaponRecovered;
    private Transform playerTransform;
    private CheckPlayerState checkPlayerState;
    private FieldOfView fov;
    private ChangeTextState changeTextState;


    public ChaseTask(FieldOfView _fov, CheckPlayerState _checkPlayerState, Transform _playerTransform, WeaponRecovered _weaponRecovered, Animator _anim, NavMeshAgent _navMeshAgent, ChangeTextState _changeTextState)
    {
        fov = _fov;
        checkPlayerState = _checkPlayerState;
        playerTransform = _playerTransform;
        weaponRecovered = _weaponRecovered;
        anim = _anim;
        navMeshAgent = _navMeshAgent;
        changeTextState = _changeTextState;
    }

    public override BTNodeStates Evaluate()
    {
        if (weaponRecovered.IsWeaponRecovered && checkPlayerState.IsPlayerDeath == false)
        {
            changeTextState.TextStringGuard = "ChaseTask::Chasing";

            Debug.Log("Chase...");

            if(Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) > 5f)
            {
                navMeshAgent.isStopped = false;
                anim.SetTrigger("isWalking");
                navMeshAgent.SetDestination(playerTransform.position);
            }
            else if(Vector3.Distance(navMeshAgent.transform.position, playerTransform.position) <= 5f)
            {
                changeTextState.TextStringGuard = "ChaseTask::Shooting";

                navMeshAgent.isStopped = true;
                anim.SetTrigger("isShooting");
            }
            Debug.Log("Is player death?: " + checkPlayerState.IsPlayerDeath);

            return BTNodeStates.SUCCES;
        }
        else if(weaponRecovered.IsWeaponRecovered && checkPlayerState.IsPlayerDeath == true)
        {
            changeTextState.TextStringGuard = "ChaseTask::KilledPlayer";

            Debug.Log("Player is Killed");
            fov.IsHumanoidVisible = false;
            weaponRecovered.IsWeaponRecovered = false;

            navMeshAgent.isStopped = false;
            anim.SetTrigger("isWalking");

            return BTNodeStates.SUCCES;
        }
        else
        {
            Debug.LogWarning("Failure Chase...");
            return BTNodeStates.FAILURE;
        }
    }
}