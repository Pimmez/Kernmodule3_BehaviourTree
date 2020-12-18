[System.Serializable]
public abstract class BTNode 
{
    protected BTNodeStates currentNodeState;

    public BTNodeStates nodeState
    {
        get { return currentNodeState; }
    }

    public BTNode() {  }

    public abstract BTNodeStates Evaluate();
}