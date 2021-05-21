
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]

public class Genome : MonoBehaviour
{
    //Max values 
     public float speed = 20;
    [HideInInspector] public float vitality = 20;
    [HideInInspector] public float mood = 20;
    [HideInInspector] public float starvingDamage = 0.1f;
    [HideInInspector] public float angryDamage = 0.1f;
    [HideInInspector] public float wanderRate = 0.001f;
    [HideInInspector] public float smellRadius = 30f;
    [HideInInspector] public float senseRadius = 30f;
    [HideInInspector] public float mutationProbability = 0.1f;
    [HideInInspector] public float strength = 0.1f;
    [HideInInspector] public float intimidationFactor = 0.5f;
    [HideInInspector] public float perceptionAccuracy = 0.5f;
    [HideInInspector] public Color color = new Color(0,0,0);
    [HideInInspector] public float threshold = 60.0f; // if hunger < threshold: search for food, else; search for partner

    //Intervals
    public int[] speed_interval = {5, 20};
    public float[] vitality_interval = {5f, 20f};
    public float[] mood_interval = { 5f, 20f };
    public float[] angryDamage_interval = { 0.05f, 0.1f };
    public float[] starvingDamage_interval = {0.05f, 0.1f};
    public float[] wanderRate_interval = {0.0001f, 0.001f};
    public float[] senseRadius_interval = {10f, 50f};
    public float[] smellRadius_interval = {10f, 50f};
    public float[] strength_interval = {0.05f, 1f};
    public float[] intimidationFactor_interval = {0.05f, 1f};
    public float[] perceptionAccuracy_interval = {0.05f, 1f};
    public float[] threshold_interval = {30f, 60f};
    public Color[] possibleColors = new Color[3];
    public int attractiveness = 0;

    private RendererFXInterface rendererFXInterface;

    private void Awake() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        GenomeWithMutations();
        rendererFXInterface.SetColor(color);
        
    }

    public void GenomeWithMutations() {
        //Speed
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            speed = Random.Range(speed_interval[0], speed_interval[1]);
        //Vitality 
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            vitality = Random.Range(vitality_interval[0], vitality_interval[1]);
        //Mood
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            mood = Random.Range(mood_interval[0], mood_interval[1]);
        //AngryDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            angryDamage = Random.Range(angryDamage_interval[0], angryDamage_interval[1]);
        //StarvingDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            starvingDamage = Random.Range(starvingDamage_interval[0], starvingDamage_interval[1]);
        //WanderRate
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            wanderRate = Random.Range(wanderRate_interval[0], wanderRate_interval[1]);
        //SenseRadius
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            senseRadius = Random.Range(senseRadius_interval[0], senseRadius_interval[1]);
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            smellRadius = Random.Range(smellRadius_interval[0], smellRadius_interval[1]);
        //Strength
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            strength = Random.Range(strength_interval[0], strength_interval[1]);
        //IntimidationFactor
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            intimidationFactor = Random.Range(intimidationFactor_interval[0], intimidationFactor_interval[1]);
        //PerceptionAccuracy
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            perceptionAccuracy = Random.Range(perceptionAccuracy_interval[0], perceptionAccuracy_interval[1]);
        //Threshold
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            threshold = Random.Range(threshold_interval[0], threshold_interval[1]);

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }

    public void BrandNewGenome(Genome go_1, Genome go_2) {
        //Speed
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            speed = Random.Range(speed_interval[0], speed_interval[1]);
        else
            speed = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.speed : go_2.speed;

        //Vitality    
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            vitality = Random.Range(vitality_interval[0], vitality_interval[1]);
        else
            vitality = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.vitality : go_2.vitality;

        //Mood
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            mood = Random.Range(mood_interval[0], mood_interval[1]);
        else
            mood = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.mood : go_2.mood;

        //AngryDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            angryDamage = Random.Range(angryDamage_interval[0], angryDamage_interval[1]);
        else
            angryDamage = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.angryDamage : go_2.angryDamage;

        //StarvingDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            starvingDamage = Random.Range(starvingDamage_interval[0], starvingDamage_interval[1]);
        else
            starvingDamage = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.starvingDamage : go_1.starvingDamage;

        //WanderRate
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            wanderRate = Random.Range(wanderRate_interval[0], wanderRate_interval[1]);
        else
            wanderRate = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.wanderRate : go_2.wanderRate;
        
        //SenseRadius
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            senseRadius = Random.Range(senseRadius_interval[0], senseRadius_interval[1]);
        else
            senseRadius = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.senseRadius : go_2.senseRadius;
        //SenseRadius
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            smellRadius = Random.Range(smellRadius_interval[0], smellRadius_interval[1]);
        else
            smellRadius = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.smellRadius : go_2.smellRadius;

        //Strength
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            strength = Random.Range(strength_interval[0], strength_interval[1]);
        else
            strength = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.strength : go_2.strength;

        //IntimidationFactor
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            intimidationFactor = Random.Range(intimidationFactor_interval[0], intimidationFactor_interval[1]);
        else
            intimidationFactor = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.intimidationFactor : go_2.intimidationFactor;
        
        //PerceptionAccuracy
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            perceptionAccuracy = Random.Range(perceptionAccuracy_interval[0], perceptionAccuracy_interval[1]);
        else
            perceptionAccuracy = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.perceptionAccuracy : go_2.perceptionAccuracy;

        //Threshold
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            threshold = Random.Range(threshold_interval[0], threshold_interval[1]);
        else
            threshold = (Random.Range(0.0f, 1.0f) >= 0.5f) ? go_1.threshold : go_2.threshold;

        attractiveness = Random.Range(0, 3);
        color = Color.red;
         UnityEngine.Debug.Log("Babyyyyyyyyyyyyyyyyy");
    }


    public void BrandNewGenomeWithParentsMeans(Genome go_1, Genome go_2) {
        //Speed
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            speed = Random.Range(speed_interval[0], speed_interval[1]);
        else
            speed = (go_1.speed + go_2.speed)/2;

        //Vitality    
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            vitality = Random.Range(vitality_interval[0], vitality_interval[1]);
        else
            vitality = (go_1.vitality + go_2.vitality)/2;

        //Mood
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            mood = Random.Range(mood_interval[0], mood_interval[1]);
        else
            mood = (go_1.mood + go_2.mood)/2;

        //AngryDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            angryDamage = Random.Range(angryDamage_interval[0], angryDamage_interval[1]);
        else
            angryDamage = (go_1.angryDamage + go_2.angryDamage)/2;

        //StarvingDamage
        if (Random.Range(0.0f, 1.0f) <= mutationProbability)
            starvingDamage = Random.Range(starvingDamage_interval[0], starvingDamage_interval[1]);
        else
            starvingDamage = (go_1.starvingDamage + go_2.starvingDamage)/2;

        //WanderRate
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            wanderRate = Random.Range(wanderRate_interval[0], wanderRate_interval[1]);
        else
            wanderRate = (go_1.wanderRate + go_2.wanderRate)/2;
        
        //SenseRadius
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            senseRadius = Random.Range(senseRadius_interval[0], senseRadius_interval[1]);
        else
            senseRadius = (go_1.senseRadius + go_2.senseRadius)/2;
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            smellRadius = Random.Range(smellRadius_interval[0], smellRadius_interval[1]);
        else
            smellRadius = (go_1.smellRadius + go_2.smellRadius)/2;

        //Strength
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            strength = Random.Range(strength_interval[0], strength_interval[1]);
        else
            strength = (go_1.strength + go_2.strength)/2;

        //IntimidationFactor
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            intimidationFactor = Random.Range(intimidationFactor_interval[0], intimidationFactor_interval[1]);
        else
            strength = (go_1.intimidationFactor + go_2.intimidationFactor)/2;
        
        //PerceptionAccuracy
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            perceptionAccuracy = Random.Range(perceptionAccuracy_interval[0], perceptionAccuracy_interval[1]);
        else
            perceptionAccuracy = (go_1.perceptionAccuracy + go_2.perceptionAccuracy)/2;
        
        //Threshold
        if(Random.Range(0.0f, 1.0f) <= mutationProbability)
            threshold = Random.Range(threshold_interval[0], threshold_interval[1]);
        else
            threshold = (go_1.threshold + go_2.threshold)/2;

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }



}


