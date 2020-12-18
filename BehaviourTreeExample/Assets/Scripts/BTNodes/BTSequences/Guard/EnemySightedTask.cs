using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemySightedTask : BTNode
{
    public static Action<bool> WeaponRecoveredEvent;

    private NavMeshAgent navMeshAgent;
    private FieldOfView fow;
    private Animator anim;
    private Transform weaponTransform;
    private WeaponRecovered weaponRecovered;
    private ChangeTextState changeTextState;


    public EnemySightedTask(WeaponRecovered _weaponRecovered, FieldOfView _fow, Animator _anim, NavMeshAgent _navMeshAgent, ChangeTextState _changeTextState)
    {
        weaponRecovered = _weaponRecovered;
        fow = _fow;
        anim = _anim;
        navMeshAgent = _navMeshAgent;
        changeTextState = _changeTextState;
    }

    public override BTNodeStates Evaluate()
    {
        if(fow.IsHumanoidVisible == true && !weaponRecovered.IsWeaponRecovered)
        {
            changeTextState.TextStringGuard = "EnemySightedTask::FindWeapon";

            Debug.Log(navMeshAgent.name + " Enemy Sighted!!");
            
            navMeshAgent.isStopped = true;

            Collider[] _colliders = Physics.OverlapSphere(navMeshAgent.transform.position, 20f, LayerMasks.Weaponry);
            foreach (Collider hitCollider in _colliders)
            {
                weaponTransform = hitCollider.transform;
                navMeshAgent.SetDestination(weaponTransform.position);
            }

            navMeshAgent.isStopped = false;
            
            if(Vector3.Distance(navMeshAgent.transform.position, weaponTransform.position) <= 2f)
            {
                Debug.Log("Found my weapon");

                navMeshAgent.isStopped = true;

                weaponTransform.gameObject.SetActive(false);

                weaponRecovered.IsWeaponRecovered = true;
            }
            else if(weaponRecovered.IsWeaponRecovered == false)
            {
                return BTNodeStates.FAILURE;
            }

            return BTNodeStates.SUCCES;
        }
        else if(fow.IsHumanoidVisible == true && weaponRecovered.IsWeaponRecovered)
        {
            return BTNodeStates.SUCCES;
        }
        else
        {
            Debug.LogWarning("EnemySighted Failure...");

            return BTNodeStates.FAILURE;
        }        
    }
}
