using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : AgentAction
{

    private float bias = 4f;
    
    public Kill(AgentBehaviour agentBehaviour) : base(agentBehaviour)
    {
        agentBehaviour.canAttack = false;
        agentBehaviour.StartCoroutine(RestoreCanFight());

    }
    public override void BeginAction()
    {

//        UnityEngine.Debug.Log("going for the kill");
    }

    
    public override void UpdateAction()
    {
        if(target == null || !agentBehaviour.canAttack)
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
//                Debug.Log("Fight");
                agentBehaviour.Fight(target);
                agentBehaviour.canAttack = false;
                agentBehaviour.canProcreate = false;
                agentBehaviour.StartCoroutine(RestoreCanFight());
                    //maybe agents lose energy
           // }
            agentBehaviour.agentIntentions.Reconsider();
        }

    }

    public IEnumerator RestoreCanFight()
    {
        yield return new WaitForSeconds(4f);
        agentBehaviour.canAttack = true;
       // agentBehaviour.canProcreate = true;
    }
    public override float Consider()
    {
        if(target == null || !agentBehaviour.canAttack)
            return 0;
        float distToTarget = (target.transform.position - agentBehaviour.transform.position).magnitude;
  
        float perception = agentBehaviour.agentDeliberation.RunOrAttackFloat(target);

        float confidence =  agentBehaviour.genome.strength.value / perception;
    
        confidence = Mathf.Clamp(confidence,0f,1f);
        return ((100-agentBehaviour.state.hunger)/100) *((100-agentBehaviour.state.hunger)/100) * (1-(distToTarget/agentBehaviour.genome.GetSenseRadius())) * confidence * agentBehaviour.genome.attackModifier.value;
    }
}
