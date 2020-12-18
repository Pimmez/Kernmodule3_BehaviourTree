using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private CheckPlayerState checkPlayerState;
    [SerializeField] Transform[] waypointTargets;
    [SerializeField] private Transform playerTarget;
   
    private NavMeshAgent agent;
    private Animator animator;
    private FieldOfView fov;
    private WeaponRecovered weaponRecovered;
    private ChangeTextState changeTextState;

    //Behaviours
    private Selecter rootAI;
    private Sequence PatrolSequence;

    private void Awake()
    {
        checkPlayerState = FindObjectOfType<CheckPlayerState>();
        changeTextState = FindObjectOfType<ChangeTextState>();
        fov = GetComponentInChildren<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        weaponRecovered = GetComponent<WeaponRecovered>();

        weaponRecovered.IsWeaponRecovered = false;
    }

    private void Start()
    {
        //Create your Behaviour Tree here!

        //Patrol Waypoints, Stand watch at waypoint
        PatrolSequence = new Sequence(new List<BTNode>
        {
            new PatrolTask(fov, animator, waypointTargets, agent, changeTextState),
            new EnemySightedTask(weaponRecovered, fov, animator, agent, changeTextState),
            new ChaseTask(fov, checkPlayerState, playerTarget, weaponRecovered, animator, agent, changeTextState),
        });

        //Root Selecter
        rootAI = new Selecter(new List<BTNode> { PatrolSequence });
    }

    private void FixedUpdate()
    {
        rootAI.Evaluate();
    }
}