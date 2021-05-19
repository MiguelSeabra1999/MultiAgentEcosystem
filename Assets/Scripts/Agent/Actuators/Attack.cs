using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private State state;
    private Genome genome;
    private Animator animator;

    private float attackValue = 100;

    void Awake()
    {
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        animator = GetComponentInChildren<Animator>();

    }

    public void AttackAgent(GameObject go)
    {
        State otherState = go.GetComponent<State>();
        
        animator.SetTrigger("Attack");

        otherState.hp -= attackValue * genome.strength;
        
        if(otherState.hp <= 0) {
            otherState.Die();
            
            UnityEngine.Debug.Log("Attacked and died");
        }
        else {
            UnityEngine.Debug.Log("Attacked");
        }        
    }
}
