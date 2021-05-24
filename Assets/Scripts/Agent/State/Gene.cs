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
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            value = Random.Range(min, max);
    }

    public void NewGene(Gene g_1, Gene g_2, float mutationProbability) {

        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            value = Random.Range(min, max);
        else
            value = (Random.Range(0.0f, 1.0f) >= 0.5f) ? g_1.value : g_2.value;
    }

    public void NewGeneWithParentsMeans(Gene g_1, Gene g_2, float mutationProbability) {
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            value = Random.Range(min, max);
        else
            value = (g_1.value + g_2.value)/2;
    }
}