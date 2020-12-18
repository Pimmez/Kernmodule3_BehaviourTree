using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTNode
{
    private List<BTNode> myNodes = new List<BTNode>();

    public Sequence(List<BTNode> _nodes)
    {
        myNodes = _nodes;
    }

    public override BTNodeStates Evaluate()
    {
        bool _childRunning = false;

        foreach (BTNode node in myNodes)
        {
            switch(node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    currentNodeState = BTNodeStates.FAILURE;
                    return currentNodeState;
               
                case BTNodeStates.SUCCES:
                    continue;
                
                case BTNodeStates.RUNNING:
                    _childRunning = true;
                    continue;

                default:
                    currentNodeState = BTNodeStates.SUCCES;
                    return currentNodeState;
            }
        }

        currentNodeState = _childRunning ? BTNodeStates.RUNNING : BTNodeStates.SUCCES;
        return currentNodeState;

    }
}