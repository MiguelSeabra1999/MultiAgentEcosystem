using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]

public class BeAttacked : MonoBehaviour
{
    private State state;
    private Animator animator;

    public float attackValue = 50;

    void Awake()
    {
        state = GetComponent<State>();
        animator = GetComponentInChildren<Animator>();

    }

    private void OnCollisionEnter(Collision other)
    {

        State otherState = other.gameObject.GetComponent<State>();
        
        animator.SetTrigger("Attack");

        if (otherState.hp < state.hp)
        {
            otherState.HealPeace(attackValue);
            otherState.Die();
        }
        else
        {
            state.Die();
        }

    }
}
