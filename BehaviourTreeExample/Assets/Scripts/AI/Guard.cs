using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private FieldOfView fov;
    private WeaponRecovered weaponRecovered;
    private ChangeTextState changeTextState;
    [SerializeField] private CheckPlayerState checkPlayerState;

    [SerializeField] Transform[] waypointTargets;
    [SerializeField] private Transform playerTarget;
   
    //Behaviours
    public Selecter rootAI;

    public Sequence PatrolSequence;

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
        //tree?.Run();
    }

    private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !animator.IsInTransition(0))
        {
            animator.CrossFade(animationName, fadeTime);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
