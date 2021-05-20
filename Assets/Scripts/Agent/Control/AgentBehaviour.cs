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
[RequireComponent(typeof(Attack))]

public class AgentBehaviour : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    private MoveActuator moveActuator;
    private State state;
    private Genome genome;
    private Smell smell;
    private Feel feel;
    private CreateAgents createAgents;
    private AgentDeliberation agentDeliberation;
    private Attack attack;
    private ParticleManager particleManager;
    private float timeSinceLastWanderShift;
    private bool firstFrame = true;

    //Procreate
    private GameObject partner = null;
    private bool followingPartner = false;
    private float procreateDelay = 5f;
    private bool canProcreate = false;

    //Attack
    private GameObject agentToAttack = null;
    private bool followingAgentToAttack = false;
    private bool canAttack = true;

    public GameObject agentPrefab;
    private float smallBias = 2f;
    private float bias = 4f;
    
    private void Awake() {
        moveActuator = GetComponent<MoveActuator>();
        state = GetComponent<State>();
        genome = GetComponent<Genome>();
        smell = GetComponent<Smell>();
        feel = GetComponent<Feel>();
        attack = GetComponent<Attack>();
        createAgents = GameObject.FindGameObjectWithTag("Environment").GetComponent<CreateAgents>();
        agentDeliberation = GetComponent<AgentDeliberation>();
        particleManager = GetComponent<ParticleManager>();

        smell.smelledFoodEvent += SmellFoodHandler;
        feel.feltAgentEvent += FeelAgentHandler;
        
    }
    void Start()
    {
        Invoke("ProcreateCooldown", procreateDelay);
    }

    private void Update() {
        if(state.blocked) return;

        if(followingAgentToAttack) {
            FollowAgentToAttack();
        }
        else if(!agentDeliberation.IsSearchingPartner()) {
            partner = null;
            followingPartner = false;
        }
        else if(followingPartner) {
            FollowPartner();   
        }
        else if(!smell.smellingFood)
            Wander();
        
        if(partner == null && followingPartner)
            followingPartner = false;
        if(agentToAttack == null && followingAgentToAttack)
            followingAgentToAttack = false;
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
    {   
        if(state.blocked) return;

        float dist = Vector3.Distance(go.transform.position, transform.position);

        if(agentDeliberation.IsSearchingPartner() && !followingPartner) {   //PROCREATE
            if(UnityEngine.Random.Range(0f, 1f) <= agentDeliberation.ProbabilityFollowToProcreate(dist)) {
                partner = go;
                followingPartner = true;
                UnityEngine.Debug.Log("Partner chosen, now following");
            }
        }
        else if(agentDeliberation.IsSearchingFood() && !smell.smellingFood && !followingAgentToAttack) { //ATTACK
            if(UnityEngine.Random.Range(0f, 1f) <= agentDeliberation.ProbabilityFollowToAttack(dist)) {
                agentToAttack = go;
                followingAgentToAttack = true;
                UnityEngine.Debug.Log("Enemy chosen, now following");
            }
        }
    }

    private void SmellFoodHandler(Vector3 pos)
    {
        if(state.blocked) return;
        if(agentDeliberation.IsSearchingFood() && !followingAgentToAttack) {
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
            float dist = Vector3.Distance(partner.transform.position, transform.position);
            Vector3 dir3D = (partner.transform.position - transform.position).normalized;
            if(dist <= smallBias)  {//agent together

                if(canProcreate && partner.GetComponent<AgentDeliberation>().YesToProcreate()) {
                    canProcreate = false;
                    Invoke("ProcreateCooldown", procreateDelay);
                    partner.GetComponent<AgentBehaviour>().BeginBabyRoutine();
                    BeginBabyRoutine();
                    GameObject baby = Instantiate(agentPrefab, new Vector3(transform.position.x + (dir3D.x/2),0,transform.position.z + (dir3D.z/2)), Quaternion.identity);
                    baby.GetComponent<Genome>().BrandNewGenome(genome, partner.GetComponent<Genome>());
                    UnityEngine.Debug.Log("Baby");
                    createAgents.NumberAgents++;
                    //maybe agents lose energy
                }
                else{
                    canProcreate = false;
                    Invoke("ProcreateCooldown", procreateDelay); 
                }
                followingPartner = false;
                partner = null;
            }
            else {//going after agent
                Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
                moveActuator.SetMovement(dir2D);
            }
        }
    }

    private void FollowAgentToAttack()
    {   
        if(agentToAttack != null) { 
            float dist = Vector3.Distance(agentToAttack.transform.position, transform.position);
            Vector3 dir3D = (agentToAttack.transform.position - transform.position).normalized;

            if(dist <= smallBias)  {//agent together
                if(canAttack) { //going to attack
                    UnityEngine.Debug.Log("Fight");
                    Fight();
                    //maybe agents lose energy
                }
                followingAgentToAttack = false;
                agentToAttack = null;
            }
            
            else {  //going after agent
                Vector2 dir2D = new Vector2(dir3D.x,dir3D.z);
                moveActuator.SetMovement(dir2D);
            }
        }
    }
    private void ProcreateCooldown()
    {
        canProcreate = true;
        state.SetBlock(false);
    }

    private void AttackCooldown()
    {
        canAttack = true;
        state.SetBlock(false);
    }
    
    [ContextMenu("PerformBabyDance")]
    private void BeginBabyRoutine()
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

    private void Fight() {
        string agent1_decision, agent2_decision;

        agent1_decision = agentDeliberation.RunOrAttack(agentToAttack);
        agent2_decision = agentToAttack.GetComponent<AgentDeliberation>().RunOrAttack(gameObject);


        if(agent1_decision == "attack" && agent2_decision == "attack") {
            state.SetBlock(true);
            agentToAttack.GetComponent<State>().SetBlock(true);

            attack.AttackAgent(agentToAttack);
            agentToAttack.GetComponent<Attack>().AttackAgent(gameObject);

            state.SetBlock(false);
            agentToAttack.GetComponent<State>().SetBlock(false);
        }
        else if(agent1_decision == "run" && agent2_decision == "attack") {
            agentToAttack.GetComponent<State>().SetBlock(true);

            if(Vector3.Distance(agentToAttack.transform.position, transform.position) <= bias)
                agentToAttack.GetComponent<Attack>().AttackAgent(gameObject);

            agentToAttack.GetComponent<State>().SetBlock(false);
        }
        else if(agent1_decision == "attack" && agent2_decision == "run") {
            state.SetBlock(true);

            if(Vector3.Distance(agentToAttack.transform.position, transform.position) <= bias)
                attack.AttackAgent(agentToAttack);

            state.SetBlock(false);
        }
        else {
            ;
        }
        followingAgentToAttack = false;
        agentToAttack = null;

        canAttack = false;
    }

    void OnDestroy()
    {
        gameplayEvents.InvokeAgentDiedEvent();
    }
    
}
