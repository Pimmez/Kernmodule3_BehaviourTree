using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private List<Transform> hidingSpots = new List<Transform>();
    
    private NavMeshAgent agent;
    private Animator animator;
    private ChangeTextState changeTextState;

    //Behaviour
    private Selecter rootAI;
    private Sequence RoqueSequence;

    private void Awake()
    {
        changeTextState = FindObjectOfType<ChangeTextState>();
        fov = FindObjectOfType<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here

        RoqueSequence = new Sequence(new List<BTNode>
        {
           new FollowPlayerTask(changeTextState, playerTransform, animator, agent),
           new PlayerSpottedTask(changeTextState, hidingSpots, fov, playerTransform, animator, agent),
        });

        //Root Selecter
        rootAI = new Selecter(new List<BTNode> { RoqueSequence });
    }

    private void FixedUpdate()
    {
        rootAI.Evaluate();
    }
}