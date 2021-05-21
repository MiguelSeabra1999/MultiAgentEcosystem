using System;
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
    public bool IsSearchingFood() {  // one 
        
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

    public string RunOrAttack(GameObject go) {
        float perception;
        float other_IntimidationFactor = go.GetComponent<Genome>().intimidationFactor;
        float other_Strength = go.GetComponent<Genome>().strength;
        //somtimes agent percepts the other agent's strength to be greater than actually is, sometimes he underestimates
        perception = genome.perceptionAccuracy * other_Strength + (1 - genome.perceptionAccuracy) * other_IntimidationFactor;

        if(genome.strength >= perception)
            return "attack";
        else
            return "run";
    }
    public float RunOrAttackFloat(GameObject go) {
        float perception;
        float other_IntimidationFactor = go.GetComponent<Genome>().intimidationFactor;
        float other_Strength = go.GetComponent<Genome>().strength;
        //somtimes agent percepts the other agent's strength to be greater than actually is, sometimes he underestimates
        perception = genome.perceptionAccuracy * other_Strength + (1 - genome.perceptionAccuracy) * other_IntimidationFactor;

        return perception;
    }

}
