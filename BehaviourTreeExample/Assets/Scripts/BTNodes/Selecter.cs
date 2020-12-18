using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : BTNode
{

    protected List<BTNode> myNodes = new List<BTNode>();

    public Selecter(List<BTNode> _nodes)
    {
        myNodes = _nodes;
    }

    public override BTNodeStates Evaluate()
    {
        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    continue;

                case BTNodeStates.SUCCES:
                    currentNodeState = BTNodeStates.SUCCES;
                    return currentNodeState;

                default:
                    continue;
            }
        }

        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;

    }
}