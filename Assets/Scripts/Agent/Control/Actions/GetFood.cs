using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : AgentAction
{
    

    public GetFood(AgentBehaviour agentBehaviour) : base(agentBehaviour)
    {}
    public override void BeginAction()
    {
        Vector3 dir3D = (target.transform.position-agentBehaviour.transform.position).normalized;
        UnityEngine.Debug.Log("going food");
        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
        agentBehaviour.moveActuator.SetMovement(dir2D);
    }

    
    public override void UpdateAction()
    {
        if(target == null)
        {
            agentBehaviour.agentIntentions.Reconsider();
            return;
        }
        Vector3 dir3D = (target.transform.position-agentBehaviour.transform.position).normalized;
       // UnityEngine.Debug.Log("going food");
        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
        agentBehaviour.moveActuator.SetMovement(dir2D);
    }
    public override float Consider()
    {

        if(target == null)
            return 0;
        float distToFood = (target.transform.position - agentBehaviour.transform.position).magnitude;
      
        return (agentBehaviour.state.hunger/100) * (1 - (distToFood/agentBehaviour.genome.smellRadius));
    }
}
