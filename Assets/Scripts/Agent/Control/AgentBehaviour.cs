using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveActuator))]
[RequireComponent(typeof(State))]
[RequireComponent(typeof(Genome))]
[RequireComponent(typeof(Smell))]


public class AgentBehaviour : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    private MoveActuator moveActuator;
    private State state;
    private Genome genome;
    private Smell smell;
    private float timeSinceLastWanderShift;
    private void Awake() {
        moveActuator = GetComponent<MoveActuator>();
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        smell = GetComponent<Smell>();

        smell.smelledFoodEvent += SmellFoodHandler;
    }

    private void Update() {
        if(!smell.smellingFood)
            Wander();
    }

    private void Wander()
    {
  
        if(Random.Range(0f,1f) < genome.wanderRate * (Time.time - timeSinceLastWanderShift))
        {
            timeSinceLastWanderShift = Time.time;
            moveActuator.SetRandomMovement();
        }
        
    }

    private void SmellFoodHandler(Vector3 pos)
    {
        Vector3 dir3D = (pos-transform.position).normalized;
        
        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
    //    Debug.Log(dir2D);
        moveActuator.SetMovement(dir2D);
    }
    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent();
    }
    
}
