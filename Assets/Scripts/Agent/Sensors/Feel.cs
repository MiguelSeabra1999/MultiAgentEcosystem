using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Genome))]
public class Feel : MonoBehaviour
{
    public float updateRate = 0.1f;
    private Genome genome;
    public bool feelingAgent = false;
    [HideInInspector] public event UnityAction<Vector3> feltAgentEvent;

    void Start()
    {
        genome = GetComponent<Genome>();
        StartCoroutine(FeelRoutine());
    }

    private IEnumerator FeelRoutine()
    {
        float minDist = Mathf.Infinity;
        Vector3 nearestAgentPos = Vector3.zero;

        while (true)
        {
            // Play a noise if an object is within the sphere's radius.
            Collider[] agents = Physics.OverlapSphere(transform.position, genome.senseRadius, LayerMask.GetMask("Agent"));
            if (agents.Length > 0)
            {
                foreach (Collider coll in agents)
                {
                    float dist = (coll.transform.position - transform.position).magnitude;
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestAgentPos = coll.transform.position;
                    }
                }

                if (feltAgentEvent != null)
                    feltAgentEvent.Invoke(nearestAgentPos);
                feelingAgent = true;
            }
            else
                feelingAgent = false;
            yield return new WaitForSeconds(updateRate);
            minDist = Mathf.Infinity;
        }
    }


}
