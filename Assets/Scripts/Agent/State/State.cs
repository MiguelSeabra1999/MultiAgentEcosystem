using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Genome))]
[RequireComponent(typeof(RendererFXInterface))]
[RequireComponent(typeof(MoveActuator))]
public class State : MonoBehaviour
{
    public float consistentHungerDrainRate = 0.01f;
    public float consistentPeaceDrainRate = 0.000000001f;
    public float hp;
    public float hunger = 100;
    public float peace = 100;
    [HideInInspector]public bool blocked = false;
    private Genome genome;
    private Animator animator;
    private Feel feel;
    private RendererFXInterface rendererFXInterface;
    private  MoveActuator moveActuator;

    private void Awake() {
        genome = GetComponent<Genome>();
        feel = GetComponent<Feel>();
        rendererFXInterface = GetComponent<RendererFXInterface>();
        animator = GetComponentInChildren<Animator>();
        moveActuator = GetComponent<MoveActuator>();
        hp = genome.vitality;
    }

    private void FixedUpdate() {
        hunger -= consistentHungerDrainRate;
        peace -= consistentPeaceDrainRate*genome.angryDamage;

        if(hunger <= 0)
        {
            hunger = 0;
            hp -= genome.starvingDamage;
        }

        if (hp <= 0)
        {
            StarveToDeath();
        }

        if (peace <= 0)
        {
            peace = 0;
            if (feel.feelingAgent)
                Attack();
        }

    }

    private void StarveToDeath()
    {
        animator.SetTrigger("Die");  
        rendererFXInterface.StartFadeOut();
        GetComponent<MoveActuator>().SetMovement(Vector2.zero);
        Invoke("DestroySelf", 2);  

    }

    private void Attack()
    {
        
        animator.SetTrigger("Attack");   

    }

    //delete starve to death
    public void Die()
    {
        animator.SetTrigger("Die");
        rendererFXInterface.StartFadeOut();
        GetComponent<MoveActuator>().SetMovement(Vector2.zero);
        Invoke("DestroySelf", 2);

    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void HealHunger(float nutricionalValue)
    {
        hunger += nutricionalValue;
        if(hunger > 100)
            hunger = 100;
    }

    public void HealPeace(float attackValue)
    {
        peace += attackValue;
        if (peace > 100)
            peace = 100;
    }

    public void SetBlock(bool state)
    {
        blocked = state;
        if(state)
        {
            moveActuator.SetMovement(Vector2.zero);
        }


    }
}
