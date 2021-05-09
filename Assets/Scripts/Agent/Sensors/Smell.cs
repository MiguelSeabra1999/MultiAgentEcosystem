using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Genome))]
public class Smell : MonoBehaviour
{
    public float updateRate  = 0.1f;
    private Genome genome;
    public bool smellingFood = false;
    [HideInInspector]public event UnityAction<Vector3> smelledFoodEvent;

    void Start()
    {
        genome = GetComponent<Genome>();
        StartCoroutine(SmellRoutine());
    }

    private IEnumerator SmellRoutine()
    {
        float minDist = Mathf.Infinity;
        Vector3 nearestFoodPos = Vector3.zero;
      
        while(true)
        {
                    // Play a noise if an object is within the sphere's radius.
            Collider[] foods = Physics.OverlapSphere(transform.position, genome.senseRadius, LayerMask.GetMask("Food"));
            if (foods.Length > 0)
            {
                foreach(Collider coll in foods)
                {
                    float dist = (coll.transform.position - transform.position).magnitude;
                    if(dist < minDist)
                    {
                        minDist = dist;
                        nearestFoodPos = coll.transform.position;
                    }
                }
                
                if(smelledFoodEvent != null)
                    smelledFoodEvent.Invoke(nearestFoodPos);
                smellingFood = true;
            }
            else
                smellingFood = false;
            yield return new WaitForSeconds(updateRate);
            minDist = Mathf.Infinity;
        }
    }
}
