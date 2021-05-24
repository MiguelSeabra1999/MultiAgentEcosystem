
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]

public class Genome : MonoBehaviour
{
    //Max values 
    [HideInInspector] public Gene speed = new Gene();
    [HideInInspector] public Gene vitality = new Gene();
    [HideInInspector] public Gene starvingDamage = new Gene();
    [HideInInspector] public Gene angryDamage = new Gene();
    [HideInInspector] public Gene wanderRate = new Gene();
    [HideInInspector] public Gene smellRadius = new Gene();
    [HideInInspector] public Gene senseRadius = new Gene();
    [HideInInspector] public float mutationProbability = 0.1f;
    [HideInInspector] public Gene strength = new Gene();
    [HideInInspector] public Gene intimidationFactor = new Gene();
    [HideInInspector] public Gene perceptionAccuracy = new Gene();
    [HideInInspector] public Color color;
    [HideInInspector] public Gene procreateModifier = new Gene();
    [HideInInspector] public Gene attackModifier = new Gene();
    [HideInInspector] public Gene minHunger = new Gene();
    public Color[] possibleColors = new Color[3];
    public int attractiveness = 0;
    private RendererFXInterface rendererFXInterface;


    private void Start() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        
        speed.min = 5f;
        speed.max = 20f;

        vitality.min = 5f;
        vitality.max = 20f;

        angryDamage.min = 0.05f;
        angryDamage.max = 0.1f;

        starvingDamage.min = 0.05f;
        starvingDamage.max = 0.1f;

        wanderRate.min = 0.0001f;
        wanderRate.max = 0.001f;

        senseRadius.min = 10f;
        senseRadius.max = 50f;

        smellRadius.min = 10f;
        smellRadius.max = 50f;

        strength.min = 0.05f;
        strength.max = 1f;

        intimidationFactor.min = 0.05f;
        intimidationFactor.max = 1f;

        perceptionAccuracy.min = 0.05f;
        perceptionAccuracy.max = 1f;

        procreateModifier.min = 0.05f;
        procreateModifier.max = 1f;

        attackModifier.min = 0.05f;
        attackModifier.max = 1f;

        minHunger.min = 50f;
        minHunger.max = 70f;

        GenomeWithMutations();

        rendererFXInterface.SetColor(color);
        
    }

    public void GenomeWithMutations() {
        //Speed
        speed.GeneWithMutations(mutationProbability);
        //Vitality 
        vitality.GeneWithMutations(mutationProbability);
        //AngryDamage
        angryDamage.GeneWithMutations(mutationProbability);
        //StarvingDamage
        starvingDamage.GeneWithMutations(mutationProbability);
        //WanderRate
        wanderRate.GeneWithMutations(mutationProbability);
        //SenseRadius
        senseRadius.GeneWithMutations(mutationProbability);
        //SmellRadius
        smellRadius.GeneWithMutations(mutationProbability);
        //Strength
        strength.GeneWithMutations(mutationProbability);
        //IntimidationFactor
        intimidationFactor.GeneWithMutations(mutationProbability);
        //PerceptionAccuracy
        perceptionAccuracy.GeneWithMutations(mutationProbability);
        //ProcreateModifier
        procreateModifier.GeneWithMutations(mutationProbability);
        //AttackModifier
        attackModifier.GeneWithMutations(mutationProbability);
        //MinHunger
        minHunger.GeneWithMutations(mutationProbability);

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }

    public void BrandNewGenome(Genome go_1, Genome go_2) {
        //Speed
        speed.NewGene(go_1.speed, go_2.speed, mutationProbability);
        //Vitality 
        vitality.NewGene(go_1.vitality, go_2.vitality, mutationProbability);
        //AngryDamage
        angryDamage.NewGene(go_1.angryDamage, go_2.angryDamage, mutationProbability);
        //StarvingDamage
        starvingDamage.NewGene(go_1.starvingDamage, go_2.starvingDamage, mutationProbability);
        //WanderRate
        wanderRate.NewGene(go_1.wanderRate, go_2.wanderRate, mutationProbability);
        //SenseRadius
        senseRadius.NewGene(go_1.senseRadius, go_2.senseRadius, mutationProbability);
        //SmellRadius
        smellRadius.NewGene(go_1.smellRadius, go_2.smellRadius, mutationProbability);
        //Strength
        strength.NewGene(go_1.strength, go_2.strength, mutationProbability);
        //IntimidationFactor
        intimidationFactor.NewGene(go_1.intimidationFactor, go_2.intimidationFactor, mutationProbability);
        //PerceptionAccuracy
        perceptionAccuracy.NewGene(go_1.perceptionAccuracy, go_2.perceptionAccuracy, mutationProbability);
        //ProcreateModifier
        procreateModifier.NewGene(go_1.procreateModifier, go_2.procreateModifier, mutationProbability);
        //AttackModifier
        attackModifier.NewGene(go_1.attackModifier, go_2.attackModifier, mutationProbability);
        //MinHunger
        minHunger.NewGene(go_1.minHunger, go_2.minHunger,mutationProbability);

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }


    public void BrandNewGenomeWithParentsMeans(Genome go_1, Genome go_2) {
        //Speed
        speed.NewGeneWithParentsMeans(go_1.speed, go_2.speed, mutationProbability);
        //Vitality 
        vitality.NewGeneWithParentsMeans(go_1.vitality, go_2.vitality, mutationProbability);
        //AngryDamage
        angryDamage.NewGeneWithParentsMeans(go_1.angryDamage, go_2.angryDamage, mutationProbability);
        //StarvingDamage
        starvingDamage.NewGeneWithParentsMeans(go_1.starvingDamage, go_2.starvingDamage, mutationProbability);
        //WanderRate
        wanderRate.NewGeneWithParentsMeans(go_1.wanderRate, go_2.wanderRate, mutationProbability);
        //SenseRadius
        senseRadius.NewGeneWithParentsMeans(go_1.senseRadius, go_2.senseRadius, mutationProbability);
        //SmellRadius
        smellRadius.NewGeneWithParentsMeans(go_1.smellRadius, go_2.smellRadius, mutationProbability);
        //Strength
        strength.NewGeneWithParentsMeans(go_1.strength, go_2.strength, mutationProbability);
        //IntimidationFactor
        intimidationFactor.NewGeneWithParentsMeans(go_1.intimidationFactor, go_2.intimidationFactor, mutationProbability);
        //PerceptionAccuracy
        perceptionAccuracy.NewGeneWithParentsMeans(go_1.perceptionAccuracy, go_2.perceptionAccuracy, mutationProbability);
        //ProcreateModifier
        procreateModifier.NewGeneWithParentsMeans(go_1.procreateModifier, go_2.procreateModifier, mutationProbability);
        //AttackModifier
        attackModifier.NewGeneWithParentsMeans(go_1.attackModifier, go_2.attackModifier, mutationProbability);
        //MinHunger
        minHunger.NewGeneWithParentsMeans(go_1.minHunger, go_2.minHunger,mutationProbability);

        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }



}


