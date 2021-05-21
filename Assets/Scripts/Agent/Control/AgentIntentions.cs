using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Smell))]
[RequireComponent(typeof(Feel))]
[RequireComponent(typeof(AgentBehaviour))]
public class AgentIntentions : MonoBehaviour
{
    private Smell smell;
    private Feel feel;
    private  AgentBehaviour agentBehaviour;
    private List<AgentAction> possibleActions = new List<AgentAction>();
    private void Awake() {
        smell = GetComponent<Smell>();
        feel = GetComponent<Feel>();
        agentBehaviour = GetComponent<AgentBehaviour>();
        smell.smelledFoodEvent += SmellFoodHandler;
        feel.feltAgentEvent += FeelAgentHandler;

        possibleActions.Add(new GetFood(agentBehaviour));
        possibleActions.Add(new Kill(agentBehaviour));
        possibleActions.Add(new Mate(agentBehaviour));

        
    }

    
    public void Reconsider()
    {

        AgentAction bestAction = null;
        float maximumDesirability = 0;
            Debug.Log("considering:");
        foreach(AgentAction agentAction in possibleActions)
        {
            float desirability = agentAction.Consider();
            Debug.Log(desirability);
            if(desirability > maximumDesirability)
            {
                maximumDesirability = desirability;
                bestAction = agentAction;
            }
        }
        if(agentBehaviour.currentAction != bestAction) 
        {
         //   Debug.Log("Got a new action of value: " + maximumDesirability);
            agentBehaviour.currentAction = bestAction;
            if(bestAction != null)
                agentBehaviour.currentAction.BeginAction();
        }

    }
   private void FeelAgentHandler(GameObject pos)
    {   
      //  Debug.Log("felt agent " + pos);
        possibleActions[1].target = pos;
        possibleActions[2].target = pos;
        Reconsider();
        
    }

    private void SmellFoodHandler(GameObject pos)
    {
      //  Debug.Log("felt food  "+ pos);
        possibleActions[0].target = pos;
        Reconsider();
        
    }
}
