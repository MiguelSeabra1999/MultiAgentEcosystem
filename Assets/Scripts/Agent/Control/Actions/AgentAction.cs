using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentAction
{
    public AgentBehaviour agentBehaviour;
    public GameObject target;
    public AgentAction(AgentBehaviour agentBehaviour)
    {
        this.agentBehaviour = agentBehaviour;
    }
    public abstract void BeginAction();

    public abstract void UpdateAction();
    public abstract float Consider();
}
