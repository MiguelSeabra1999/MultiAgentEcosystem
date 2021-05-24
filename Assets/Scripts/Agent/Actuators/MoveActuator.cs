using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Genome))]
[RequireComponent(typeof(State))]
public class MoveActuator : MonoBehaviour
{
    public float walkingEnergyDrainRate = 0.1f;
    private Rigidbody rb;
    private Animator animator;
    private Genome genome;
    private State state;
    private float turnSmoothVelocity;
    private UnityEngine.Vector2 currentDir = UnityEngine.Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        genome = GetComponent<Genome>();
        state = GetComponent<State>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate() {
        rb.angularVelocity = UnityEngine.Vector3.zero;
        if(currentDir.magnitude > 0.01f)
        {
            AdjustFacingDirection(currentDir);
            state.hunger -= walkingEnergyDrainRate;
            Debug.DrawRay(transform.position,(new UnityEngine.Vector3(currentDir.x,0,currentDir.y)).normalized * genome.GetSenseRadius(), Color.blue);
        }else
            rb.velocity = UnityEngine.Vector3.zero;
    }

    [ContextMenu("MoveForwards")]
    private void MoveForwards()
    {
  
        SetMovement(new UnityEngine.Vector2(0,1));
    }

    [ContextMenu("Stop")]
    private void StopMoving()
    {
  
        SetMovement(UnityEngine.Vector2.zero);
    }

    public void SetMovement(UnityEngine.Vector2 move)
    {

        rb.velocity = new UnityEngine.Vector3(move.x,0,move.y) * genome.speed.value;


        currentDir = move;
        animator.SetFloat("Speed", genome.speed.value);

        if(move.magnitude > 0.01f && !animator.GetBool("IsWalking") )
            animator.SetBool("IsWalking", true);
        else if(move.magnitude <= 0.01f && animator.GetBool("IsWalking"))
            animator.SetBool("IsWalking", false);
    }

    public void SetRandomMovement()
    {
        SetMovement(RandomDirection());
    }

    private void AdjustFacingDirection(UnityEngine.Vector2 dir)
    {
        
        float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.5f);
        transform.rotation = UnityEngine.Quaternion.Euler(0f, angle, 0f);
    }


    private UnityEngine.Vector2 RandomDirection()
    {
        UnityEngine.Vector2 dir = new UnityEngine.Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
        return dir.normalized;
    }
}
