using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : AgentAction
{

    private float bias = 4f;
    public Mate(AgentBehaviour agentBehaviour) : base(agentBehaviour)
    {}
    public override void BeginAction()
    {
        UnityEngine.Debug.Log("going for the pickup");
    }

    
    public override void UpdateAction()
    {
        if(target == null)
        {
            agentBehaviour.agentIntentions.Reconsider();
            return;
        }
        Vector3 dir3D = (target.transform.position-agentBehaviour.transform.position).normalized;

        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
        agentBehaviour.moveActuator.SetMovement(dir2D);
        float distToTarget = (target.transform.position - agentBehaviour.transform.position).magnitude;
        if(distToTarget <= bias)  {//agent together
        Debug.Log("proposing");
             if(agentBehaviour.canProcreate && target.GetComponent<AgentDeliberation>().YesToProcreate()) {
                 agentBehaviour.Procreate(target);
             }
             else{

                 agentBehaviour.GetDumped();
             }
             agentBehaviour.agentIntentions.Reconsider();

         }

    }
    public override float Consider()
    {
        if(target == null || !agentBehaviour.canProcreate || agentBehaviour.state.hunger <= 50)
            return 0;
        float distToTarget = (target.transform.position - agentBehaviour.transform.position).magnitude;
   
        return(agentBehaviour.state.hunger/100) * (1-(distToTarget/agentBehaviour.genome.GetSmellRadius())) * agentBehaviour.genome.procreateModifier.value;
    }

}
