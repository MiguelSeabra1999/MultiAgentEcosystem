using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveActuator))]
[RequireComponent(typeof(State))]
[RequireComponent(typeof(Genome))]
[RequireComponent(typeof(Smell))]
[RequireComponent(typeof(AgentDeliberation))]


public class AgentBehaviour : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    private MoveActuator moveActuator;
    private State state;
    private Genome genome;
    private Smell smell;
    private Feel feel;
    private AgentDeliberation agentDeliberation;
    private float timeSinceLastWanderShift;
    private bool firstFrame = true;

    private GameObject partner = null;
    private bool followingPartner = false;
    private void Awake() {
        moveActuator = GetComponent<MoveActuator>();
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        smell = GetComponent<Smell>();
        feel = GetComponent<Feel>();
        agentDeliberation = GetComponent<AgentDeliberation>();

        smell.smelledFoodEvent += SmellFoodHandler;
        feel.feltAgentEvent += FeelAgentHandler;
    }

    private void Update() {
        if(!agentDeliberation.IsSearchingPartner(state.hunger)) {
            partner = null;
            followingPartner = false;
        }
        if(!smell.smellingFood && !feel.feelingAgent && !followingPartner)
            Wander();
        if(followingPartner) {
            FollowPartner();
        }  
    }

    private void Wander()
    {
  
        if(firstFrame || UnityEngine.Random.Range(0f,1f) < genome.wanderRate * (Time.time - timeSinceLastWanderShift))
        {
            timeSinceLastWanderShift = Time.time;
            moveActuator.SetRandomMovement();
            firstFrame= false;
        }
        
    }

    private void FeelAgentHandler(GameObject go)
    {   if(agentDeliberation.IsSearchingPartner(state.hunger) && !followingPartner) {
            partner = go;
            followingPartner = true;
        }
    }

    private void SmellFoodHandler(Vector3 pos)
    {
        if(agentDeliberation.IsSearchingFood(state.hunger)) {
            Vector3 dir3D = (pos-transform.position).normalized;
            UnityEngine.Debug.Log("going food");
            Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
            moveActuator.SetMovement(dir2D);
        }
    }

    private void FollowPartner()
    {
        if(partner != null) {
            Vector3 dir3D = (partner.transform.position - transform.position).normalized;
            UnityEngine.Debug.Log("going partner:" + dir3D);
            Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
            moveActuator.SetMovement(dir2D);
        }
    }

    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent();
    }
    
}
