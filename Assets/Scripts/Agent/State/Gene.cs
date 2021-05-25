using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
   public string name;
    public float value;
    public float min;
    public float max;

    public void GeneWithMutations(float mutationProbability) {

        value = Random.Range(min, max);
    }

    public void NewGene(Gene g_1, Gene g_2, float mutationProbability) {

        value = (Random.Range(0.0f, 1.0f) >= 0.5f) ? g_1.value : g_2.value;
  
        int dir = (Random.Range(0.0f, 1.0f) >= 0.5f) ? -1 : 1;
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            value = Mathf.Clamp(value + dir*(max-min)*(RandomGaussian()/2) ,min,max);
        
    }

    public void NewGeneWithParentsMeans(Gene g_1, Gene g_2, float mutationProbability) {
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            value = Random.Range(min, max);
        else
            value = (g_1.value + g_2.value)/2;
    }

    public float RandomGaussian(float minValue = 0, float maxValue = 1.0f)
    {
        float u, v, S;
    
        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);
    
        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
    
        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

}