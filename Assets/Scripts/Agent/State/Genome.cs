using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]
public class Genome : MonoBehaviour
{
    //Max values 
    public float speed = 20;
    public float vitality = 20;
    public float starvingDamage = 0.1f;
    public float wanderRate = 0.001f;
    public float senseRadius = 7f;
    public float mutationProbability = 1f;

    //Intervals
    /*private int[] speed_interval = int[5, 20];
    private float[] vitality_interval = new float[5f, 20f];
    private float[] starvingDamage_interval = new float[0.05f, 0.1f];
    private float[] wanderRate_interval = new float[0.0001f, 0.001f];
    private float[] senseRadius_interval = new float[2f, 7f];
    private float[] mutationProbability_interval = new float[0.05, 1f];
    */

    public Color color;

    private RendererFXInterface rendererFXInterface;
    private void Awake() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        rendererFXInterface.SetColor(color);
        //genomeWithMutations();
    }
/*
    public void genomeWithMutations() {
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            speed = Random.range(speed_interval[0], speed_interval[1]);
            
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            vitality = Random.range(vitality_interval[0], vitality_interval[1]);
        
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            starvingDamage = Random.range(starvingDamage_interval[0], starvingDamage_interval[1]);
        
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            wanderRate = Random.range(wanderRate_interval[0], wanderRate_interval[1]);
        
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            senseRadius = Random.range(senseRadius_interval[0], senseRadius_interval[1]);
        
        if(Random.range(0.0f, 1.0f) <= mutationProbability)
            mutationProbability = Random.range(mutationProbability_interval[0], mutationProbability_interval[1]);
    }*/


}


