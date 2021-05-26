
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]

public class Genome : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    //Max values 
    [HideInInspector] public Gene speed;
    [HideInInspector] public Gene vitality;
    [HideInInspector] public Gene starvingDamage;
   // [HideInInspector] public Gene angryDamage;
    [HideInInspector] public Gene wanderRate;
    [HideInInspector] public Gene smellToSenseRatio;
    [HideInInspector] public Gene strength;
    [HideInInspector] public Gene intimidationFactor;
    [HideInInspector] public Gene perceptionAccuracy;
    [HideInInspector] public Gene procreateModifier;
    [HideInInspector] public Gene attackModifier;
    [HideInInspector] public Gene minHunger;
    [HideInInspector] public float mutationProbability = 0.02f;
    [HideInInspector] public Color color;
    public Color[] possibleColors = new Color[3];
    public int attractiveness = 0;
    private RendererFXInterface rendererFXInterface;
    private float totalRadius = 50;
    private List<Gene> genes = new List<Gene>();
    private float birthDate;
    [HideInInspector]public int children = 0;
    private void Awake() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        gameplayEvents.saveDataEvent += SaveData;

        speed = new Gene();
        vitality = new Gene();
        starvingDamage = new Gene();
        wanderRate = new Gene();
        smellToSenseRatio = new Gene();
        strength = new Gene();
        intimidationFactor = new Gene();
        perceptionAccuracy = new Gene();
        procreateModifier = new Gene();
        attackModifier = new Gene();
        minHunger = new Gene();

        genes.Add(speed);
        genes.Add(vitality);
        genes.Add(starvingDamage);
        genes.Add(wanderRate);
        genes.Add(smellToSenseRatio);
        genes.Add(strength);
        genes.Add(intimidationFactor);
        genes.Add(perceptionAccuracy);
        genes.Add(procreateModifier);
        genes.Add(attackModifier);
        genes.Add(minHunger);

        speed.min = 5f;
        speed.max = 20f;

        vitality.min = 5f;
        vitality.max = 20f;

       // angryDamage.min = 0.05f;
      //  angryDamage.max = 0.1f;

        starvingDamage.min = 0.05f;
        starvingDamage.max = 0.1f;

        wanderRate.min = 0.0001f;
        wanderRate.max = 0.001f;

        smellToSenseRatio.min = 0f;
        smellToSenseRatio.max = 1f;


        strength.min = 0.05f;
        strength.max = 1f;

        intimidationFactor.min = 0.05f;
        intimidationFactor.max = 1f;

        perceptionAccuracy.min = 0.05f;
        perceptionAccuracy.max = 1f;

        procreateModifier.min = 0.5f;
        procreateModifier.max = 1.5f;

        attackModifier.min = 0.5f;
        attackModifier.max = 1.5f;

        minHunger.min = 20f;
        minHunger.max = 60f;

        GenomeWithMutations();

        rendererFXInterface.SetColor(color);
        
    }
    private void Start() {
        birthDate = Time.time;
    }
    public void GenomeWithMutations() {
        foreach(Gene gene in genes)
            gene.GeneWithMutations(mutationProbability);
        attractiveness = Random.Range(0, 3);
        color = possibleColors[attractiveness];
    }

    private void OnDestroy()
    {
        gameplayEvents.saveDataEvent -= SaveData;
    }

    public void BrandNewGenome(Genome go_1, Genome go_2) {
        //Speed
        speed.NewGene(go_1.speed, go_2.speed, mutationProbability);
        //Vitality 
        vitality.NewGene(go_1.vitality, go_2.vitality, mutationProbability);
        //AngryDamage
     //   angryDamage.NewGene(go_1.angryDamage, go_2.angryDamage, mutationProbability);
        //StarvingDamage
        starvingDamage.NewGene(go_1.starvingDamage, go_2.starvingDamage, mutationProbability);
        //WanderRate
        wanderRate.NewGene(go_1.wanderRate, go_2.wanderRate, mutationProbability);
        //smellToSenseRatio
        smellToSenseRatio.NewGene(go_1.smellToSenseRatio, go_2.smellToSenseRatio, mutationProbability);
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
        children++;
    }


    public void BrandNewGenomeWithParentsMeans(Genome go_1, Genome go_2) {
        //Speed
        speed.NewGeneWithParentsMeans(go_1.speed, go_2.speed, mutationProbability);
        //Vitality 
        vitality.NewGeneWithParentsMeans(go_1.vitality, go_2.vitality, mutationProbability);
        //AngryDamage
     //   angryDamage.NewGeneWithParentsMeans(go_1.angryDamage, go_2.angryDamage, mutationProbability);
        //StarvingDamage
        starvingDamage.NewGeneWithParentsMeans(go_1.starvingDamage, go_2.starvingDamage, mutationProbability);
        //WanderRate
        wanderRate.NewGeneWithParentsMeans(go_1.wanderRate, go_2.wanderRate, mutationProbability);
        //smellToSenseRatio
        smellToSenseRatio.NewGeneWithParentsMeans(go_1.smellToSenseRatio, go_2.smellToSenseRatio, mutationProbability);
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

    public float GetSenseRadius()
    {
        return smellToSenseRatio.value * totalRadius;
    }
    public float GetSmellRadius()
    {
        return (1 - smellToSenseRatio.value) * totalRadius;
    }

    public void SaveData()
    {
        //if(gameObject == null) return;
        string myData = "";
        foreach(Gene gene in genes)
            myData += gene.value + " ";

        gameplayEvents.genomeData.Add(myData);
    }
    public string GetObituary()
    {
        string myData = "";
        myData += (Time.time - birthDate) + " ";
        myData += children + " ";
        foreach(Gene gene in genes)
            myData += gene.value + " ";
        return myData;
    }

}


