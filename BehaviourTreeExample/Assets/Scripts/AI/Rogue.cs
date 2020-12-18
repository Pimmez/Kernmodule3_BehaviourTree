using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    //private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private List<Transform> hidingSpots = new List<Transform>();

    //Behaviour
    public Selecter rootAI;
    public Sequence RoqueSequence;

    private void Awake()
    {
        fov = FindObjectOfType<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here

        RoqueSequence = new Sequence(new List<BTNode>
        {
           new FollowPlayerTask(playerTransform, animator, agent),
           new PlayerSpottedTask(hidingSpots, fov, playerTransform, animator, agent),
        });


        //Root Selecter
        rootAI = new Selecter(new List<BTNode> { RoqueSequence });
    }

    private void FixedUpdate()
    {
        rootAI.Evaluate();
        //tree?.Run();
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
