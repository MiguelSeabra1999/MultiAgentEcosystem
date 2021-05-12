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
    private Feel feel;
    private float timeSinceLastWanderShift;
    private bool firstFrame = true;
    private void Awake() {
        moveActuator = GetComponent<MoveActuator>();
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        smell = GetComponent<Smell>();
        feel = GetComponent<Feel>();

        smell.smelledFoodEvent += SmellFoodHandler;
        feel.feltAgentEvent += FeelAgentHandler;
    }

    private void Update() {
        if(!smell.smellingFood)
            Wander();
    }

    private void Wander()
    {
  
        if(firstFrame || Random.Range(0f,1f) < genome.wanderRate * (Time.time - timeSinceLastWanderShift))
        {
            timeSinceLastWanderShift = Time.time;
            moveActuator.SetRandomMovement();
            firstFrame= false;
        }
        
    }

    private void FeelAgentHandler(Vector3 pos)
    {   if(!smell.smellingFood) {
            Vector3 dir3D = (pos - transform.position).normalized;

            Vector2 dir2D = new Vector2(dir3D.x, dir3D.z);
            Debug.Log(dir2D);
            moveActuator.SetMovement(dir2D);
        }
    }

    private void SmellFoodHandler(Vector3 pos)
    {
        Vector3 dir3D = (pos-transform.position).normalized;
        
        Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
        moveActuator.SetMovement(dir2D);
    }

    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent();
    }
    
}
