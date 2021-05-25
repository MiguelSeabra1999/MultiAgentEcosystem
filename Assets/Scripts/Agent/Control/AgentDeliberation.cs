using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeliberation : MonoBehaviour
{

    private State state;
    private Genome genome;
    private AgentBehaviour agentBehaviour;

    void Awake() {
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        agentBehaviour = GetComponent<AgentBehaviour>();
    }
    /*
    public bool IsSearchingFood() {  // one 
        
        return (state.hunger <= genome.threshold);
    }

    public bool IsSearchingPartner() {
        return (state.hunger > genome.threshold);
    }*/



    
    public bool YesToProcreate() {
        return state.hunger > genome.minHunger.value && agentBehaviour.canProcreate;
    }

    public string RunOrAttack(GameObject go) {
        float perception;
        Genome otherGenome = go.GetComponent<Genome>();
        float other_IntimidationFactor = otherGenome.intimidationFactor.value;
        float other_Strength = otherGenome.strength.value;
        //somtimes agent percepts the other agent's strength to be greater than actually is, sometimes he underestimates
        perception = genome.perceptionAccuracy.value * other_Strength + (1 - genome.perceptionAccuracy.value) * other_IntimidationFactor;

        if(genome.strength.value >= perception)
            return "attack";
        else
            return "run";
    }
    public float RunOrAttackFloat(GameObject go) {
        float perception;
        Genome otherGenome = go.GetComponent<Genome>();
        float other_IntimidationFactor = otherGenome.intimidationFactor.value;
        float other_Strength = otherGenome.strength.value;
        //somtimes agent percepts the other agent's strength to be greater than actually is, sometimes he underestimates
        perception = genome.perceptionAccuracy.value * other_Strength + (1 - genome.perceptionAccuracy.value) * other_IntimidationFactor;

        return perception;
    }

}
