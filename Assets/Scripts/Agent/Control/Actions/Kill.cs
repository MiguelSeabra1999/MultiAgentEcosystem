using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : AgentAction
{

    private float bias = 4f;
    private bool canFight = true;
    public Kill(AgentBehaviour agentBehaviour) : base(agentBehaviour)
    {}
    public override void BeginAction()
    {

        UnityEngine.Debug.Log("going for the kill");
    }

    
    public override void UpdateAction()
    {
        if(target == null || !canFight)
        {
            agentBehaviour.agentIntentions.Reconsider();
            return;
        }
        Vector3 dir3D = (target.transform.position-agentBehaviour.transform.position).normalized;

        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
        agentBehaviour.moveActuator.SetMovement(dir2D);

        float distToTarget = (target.transform.position - agentBehaviour.transform.position).magnitude;
        if(distToTarget <= bias)  {//agent together
          //  if(!agentBehaviour.state.blocked) { //going to attack
                Debug.Log("Fight");
                agentBehaviour.Fight(target);
                canFight = false;
                agentBehaviour.StartCoroutine(RestoreCanFight());
                    //maybe agents lose energy
           // }
            agentBehaviour.agentIntentions.Reconsider();
        }

    }

    public IEnumerator RestoreCanFight()
    {
        yield return new WaitForSeconds(2f);
        canFight = true;
    }
    public override float Consider()
    {
        if(target == null || !canFight)
            return 0;
        float distToTarget = (target.transform.position - agentBehaviour.transform.position).magnitude;
  
        float perception = agentBehaviour.agentDeliberation.RunOrAttackFloat(target);

        float confidence =  agentBehaviour.genome.strength / perception;
    
        confidence = Mathf.Clamp(confidence,0f,1f);
        return ((100-agentBehaviour.state.hunger)/100) * (1-(distToTarget/agentBehaviour.genome.senseRadius)) * confidence;
    }
}
