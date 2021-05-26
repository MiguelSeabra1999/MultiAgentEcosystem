using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveActuator))]
[RequireComponent(typeof(State))]
[RequireComponent(typeof(Genome))]

[RequireComponent(typeof(AgentDeliberation))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(AgentIntentions))]


public class AgentBehaviour : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    [HideInInspector] public MoveActuator moveActuator;
    [HideInInspector] public State state;
    [HideInInspector] public Genome genome;
    [HideInInspector] public AgentIntentions agentIntentions;
    private CreateAgents createAgents;
    [HideInInspector] public AgentDeliberation agentDeliberation;
    private Attack attack;
    private ParticleManager particleManager;
    [HideInInspector] public bool canProcreate = true;
    [HideInInspector] public bool canAttack = true;
    private float timeSinceLastWanderShift;
    private bool firstFrame = true;
    private float procreateDelay = 6f;
    private float dumpedProcreateDelay = 2f;
    private float bias = 4f;

    public GameObject agentPrefab;


    [HideInInspector] public AgentAction currentAction = null;
    
    private void Awake() {
        moveActuator = GetComponent<MoveActuator>();
        state = GetComponent<State>();
        genome = GetComponent<Genome>();

        attack = GetComponent<Attack>();
        agentIntentions = GetComponent<AgentIntentions>();
        createAgents = GameObject.FindGameObjectWithTag("Environment").GetComponent<CreateAgents>();
        agentDeliberation = GetComponent<AgentDeliberation>();
        particleManager = GetComponent<ParticleManager>();


    }
    void Start()
    {
        Invoke("ProcreateCooldown", procreateDelay);
        Invoke("ProcreateCooldown", procreateDelay);
    }

    private void Update() {
        if(state.blocked) return;
        if(currentAction != null)
            currentAction.UpdateAction();
        else    
        {
            Wander();
            //agentIntentions.Reconsider();
        }
    }

    private void Wander()
    {
        
        if(firstFrame || UnityEngine.Random.Range(0f,1f) < genome.wanderRate.value * (Time.time - timeSinceLastWanderShift))
        {
            timeSinceLastWanderShift = Time.time;
            moveActuator.SetRandomMovement();
            firstFrame= false;
        }
        
    }
    
    [ContextMenu("PerformBabyDance")]
    public void BeginBabyRoutine()
    {
        if(!state.blocked)
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


        transform.rotation = Quaternion.Euler(transform.rotation.x,normalAngle,transform.rotation.z);
        state.SetBlock(false);
    }

    public void Procreate(GameObject partner)
    {
          Vector3 dir3D = (partner.transform.position-transform.position).normalized;

            canProcreate = false;
            canAttack = false;
            Invoke("ProcreateCooldown", procreateDelay);
            partner.GetComponent<AgentBehaviour>().BeginBabyRoutine();
            BeginBabyRoutine();
            GameObject baby = Instantiate(agentPrefab, new Vector3(transform.position.x + (dir3D.x/2),0,transform.position.z + (dir3D.z/2)), Quaternion.identity);
            baby.GetComponent<Genome>().BrandNewGenome(genome, partner.GetComponent<Genome>());
//            UnityEngine.Debug.Log("Baby");
            createAgents.NumberAgents++;

        state.hunger -= 10;
        partner.GetComponent<State>().hunger -= 10;
    }

    public void GetDumped()
    {
       // UnityEngine.Debug.Log("dumped");
        canProcreate = false;
        Invoke("ProcreateCooldown", dumpedProcreateDelay); 
    }
    private void ProcreateCooldown()
    {
        canProcreate = true;
        canAttack = true;
        state.SetBlock(false);
    }

    public void Fight(GameObject agentToAttack) {
        string agent1_decision, agent2_decision;
        if(agentToAttack == null) return;

        agent1_decision = agentDeliberation.RunOrAttack(agentToAttack);
        agent2_decision = agentToAttack.GetComponent<AgentDeliberation>().RunOrAttack(gameObject);
//        UnityEngine.Debug.Log("startingFight " + state.hunger,gameObject);
        State agentToAttackState = agentToAttack.GetComponent<State>();
        Attack agentToAttackAttack = agentToAttack.GetComponent<Attack>();
        if(agent1_decision == "attack" && agent2_decision == "attack") {
            state.SetBlock(true);
            agentToAttackState.SetBlock(true);

            attack.AttackAgent(agentToAttack);
            agentToAttackAttack.AttackAgent(gameObject);

            StartCoroutine(QueueUnblock(state));
            StartCoroutine(QueueUnblock(agentToAttackState));
    
        }
        else if(agent1_decision == "run" && agent2_decision == "attack") {
            agentToAttackState.SetBlock(true);

            if(Vector3.Distance(agentToAttack.transform.position, transform.position) <= bias)
                agentToAttackAttack.AttackAgent(gameObject);
            

            transform.SendMessage("RunFromAttacker",agentToAttack);//implemented on move actuator
            StartCoroutine(QueueUnblock(agentToAttackState));
        }
        else if(agent1_decision == "attack" && agent2_decision == "run") {
            state.SetBlock(true);

            if(Vector3.Distance(agentToAttack.transform.position, transform.position) <= bias)
                attack.AttackAgent(agentToAttack);
            agentToAttack.transform.SendMessage("RunFromAttacker",gameObject);//implemented on move actuator
            StartCoroutine(QueueUnblock(state));
        }
        agentIntentions.Reconsider();
    }

    private IEnumerator QueueUnblock(State state)
    {
        yield return new WaitForSeconds(1f);
        state.SetBlock(false);
        agentIntentions.Reconsider();
    }
  
    private Vector2 Reflect(Vector2 forwardDir, Vector2 normal)
    {
        return forwardDir - 2f * Vector2.Dot(forwardDir,normal)*normal;
    }

    private Vector2 Convert3Dto2D(Vector3 vec)
    {
        return new Vector2(vec.x,vec.z);
    }
    private Vector3 Convert2Dto3D(Vector2 vec)
    {
        return new Vector3(vec.x,0,vec.y);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Vector2 newDir = Reflect(Convert3Dto2D(transform.forward),Convert3Dto2D(other.contacts[0].normal));
            moveActuator.SetMovement(newDir);
        }
    }
    

    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent(genome);
    }
    
}
