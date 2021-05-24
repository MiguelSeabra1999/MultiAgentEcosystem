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
    [HideInInspector]public event UnityAction<GameObject> smelledFoodEvent;

    void Start()
    {
        genome = GetComponent<Genome>();
        StartCoroutine(SmellRoutine());
    }

    private IEnumerator SmellRoutine()
    {
        float minDist = Mathf.Infinity;
        GameObject nearestFood= null;
      
        while(true)
        {
                    // Play a noise if an object is within the sphere's radius.
            Collider[] foods = Physics.OverlapSphere(transform.position, genome.smellRadius.value, LayerMask.GetMask("Food"));
            if (foods.Length > 0)
            {
                foreach(Collider coll in foods)
                {
                    float dist = (coll.transform.position - transform.position).magnitude;
                    if(dist < minDist)
                    {
                        minDist = dist;
                        nearestFood = coll.gameObject;
                    }
                }
                
                if(smelledFoodEvent != null)
                    smelledFoodEvent.Invoke(nearestFood);
                smellingFood = true;
            }
            else
                smellingFood = false;
            yield return new WaitForSeconds(updateRate);
            minDist = Mathf.Infinity;
        }
    }
}
