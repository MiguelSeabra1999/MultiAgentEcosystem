using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeliberation : MonoBehaviour
{
    public bool IsSearchingFood(float hunger, float threshold) {
        return (hunger <= threshold);
    }

    public bool IsSearchingPartner(float hunger, float threshold) {
        return (hunger > threshold);
    }

    public float ProbabilityFollowToProcreate(float hunger, float dist, float threshold) { // gives likelihood
        float likelihood_follow_partner;

        if (dist >= 0 && dist < 1)
            dist = 1;
        else if(dist > 1)
            dist = Mathf.Min(dist, 100);
      
        likelihood_follow_partner = (hunger/100) * (1 / dist);
           
        return likelihood_follow_partner;
    }

    public float ProbabilityFollowToAttack(float hunger, float dist, float threshold) { // gives likelihood
        float likelihood_attack;

        if (dist >= 0 && dist < 1)
            dist = 1;
        else if(dist > 1)
            dist = Mathf.Min(dist, 100);
        
        likelihood_attack = (100 - hunger)/100 * (1 / dist);
           
        return likelihood_attack;
    }

    public bool YesToProcreate(float hunger, float threshold) {
        return hunger > threshold;
    }


}
