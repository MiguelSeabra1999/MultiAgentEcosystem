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
    private ParticleManager particleManager;
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
        particleManager = GetComponent<ParticleManager>();

        smell.smelledFoodEvent += SmellFoodHandler;
        feel.feltAgentEvent += FeelAgentHandler;
    }

    private void Update() {
        if(!agentDeliberation.IsSearchingPartner(state.hunger, genome.threshold)) {
            partner = null;
            followingPartner = false;
        }
        if((!smell.smellingFood || !agentDeliberation.IsSearchingFood(state.hunger)) && /*!feel.feelingAgent && */!followingPartner)
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
    {   if(agentDeliberation.IsSearchingPartner(state.hunger, genome.threshold) && !followingPartner) {
            float dist = Vector3.Distance(go.transform.position, transform.position);
            UnityEngine.Debug.Log("prob: " + agentDeliberation.ProbabilityFollowToProcreate(state.hunger, dist, genome.threshold));
            if(UnityEngine.Random.Range(0f, 1f) <= agentDeliberation.ProbabilityFollowToProcreate(state.hunger, dist, genome.threshold)) {
                UnityEngine.Debug.Log("accepted");
                partner = go;
                followingPartner = true;
            }
        }
    }

    private void SmellFoodHandler(Vector3 pos)
    {
        if(agentDeliberation.IsSearchingFood(state.hunger, genome.threshold)) {
            Vector3 dir3D = (pos-transform.position).normalized;
            UnityEngine.Debug.Log("going food");
            Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
            moveActuator.SetMovement(dir2D);
        }else
        {
            UnityEngine.Debug.Log("I sense food but I'm not hungry");
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
    [ContextMenu("PerformBabyDance")]
    private void BeginBabyRoutine()
    {
        StartCoroutine(BabyDanceRoutine());
    }

    private IEnumerator BabyDanceRoutine()
    {
        float duration = 2f;
        float startTime = Time.time;
        float swingDir = 1;
        state.SetBlock(true);
        if(particleManager != null)
            particleManager.PlayHearts();
        float normalAngle = transform.eulerAngles.y;
        while(Time.time  - startTime  < duration)
        {
            float percent = 0;
            float swingDuration = 0.3f;
            float swingOffset = 60f;
            float startSwingTime = Time.time;
            float startAngle = transform.eulerAngles.y;
            while(percent < 1)
            {
                percent = (Time.time - startSwingTime)/swingDuration;
                float currAngle = Mathf.LerpAngle(startAngle, normalAngle + swingOffset*swingDir, percent);
                transform.rotation = Quaternion.Euler(transform.rotation.x,currAngle,transform.rotation.z);
                yield return null;
            }
            swingDir *=-1;
        }

        state.SetBlock(false);
    }

    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent();
    }
    
}
