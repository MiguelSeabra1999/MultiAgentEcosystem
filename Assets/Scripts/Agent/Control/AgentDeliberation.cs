using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeliberation : MonoBehaviour
{

    private State state;
    private Genome genome;

    void Awake() {
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
    }
    public bool IsSearchingFood() {
        return (state.hunger <= genome.threshold);
    }

    public bool IsSearchingPartner() {
        return (state.hunger > genome.threshold);
    }

    public float ProbabilityFollowToProcreate(float dist) { 
        float likelihood_follow_partner;

        if (dist >= 0 && dist < 1)
            dist = 1;
        else if(dist > 1)
            dist = Mathf.Min(dist, 100);
      
        likelihood_follow_partner = (state.hunger/100) * (1 / dist);
           
        return likelihood_follow_partner;
    }

    public float ProbabilityFollowToAttack( float dist) { 
        float likelihood_attack;

        if (dist >= 0 && dist < 1)
            dist = 1;
        else if(dist > 1)
            dist = Mathf.Min(dist, 100);
        
        likelihood_attack = (100 - state.hunger)/100 * (1 / dist);
           
        return likelihood_attack;
    }

    public bool YesToProcreate() {
        return state.hunger > genome.threshold;
    }


}
