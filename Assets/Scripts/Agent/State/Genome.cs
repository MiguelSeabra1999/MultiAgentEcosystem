
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]

public class Genome : MonoBehaviour
{
    //Max values 
    
    [HideInInspector] public float speed = 20;
    [HideInInspector] public float vitality = 20;
    [HideInInspector] public float starvingDamage = 0.1f;
    [HideInInspector] public float wanderRate = 0.001f;
    [HideInInspector] public float senseRadius = 30f;
    [HideInInspector] public float mutationProbability = 1f;

    [HideInInspector] public Color color = new Color(0,0,0);

    //Intervals
    public int[] speed_interval = {5, 20};
    public float[] vitality_interval = {5f, 20f};
    public float[] starvingDamage_interval = {0.05f, 0.1f};
    public float[] wanderRate_interval = {0.0001f, 0.001f};
    public float[] senseRadius_interval = {40f, 100f};
    public float[] mutationProbability_interval = {0.05f, 1f};
    public Color[] possibleColors = new Color[3];
    public int attractiveness = 0;

    private RendererFXInterface rendererFXInterface;

    private void Awake() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        GenomeWithMutations();
        rendererFXInterface.SetColor(color);
        
    }

    public void GenomeWithMutations() {
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            speed = Random.Range(speed_interval[0], speed_interval[1]);
            
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            vitality = Random.Range(vitality_interval[0], vitality_interval[1]);
        
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            starvingDamage = Random.Range(starvingDamage_interval[0], starvingDamage_interval[1]);
        
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            wanderRate = Random.Range(wanderRate_interval[0], wanderRate_interval[1]);
        
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            senseRadius = Random.Range(senseRadius_interval[0], senseRadius_interval[1]);
        
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            mutationProbability = Random.Range(mutationProbability_interval[0], mutationProbability_interval[1]);

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }



}


