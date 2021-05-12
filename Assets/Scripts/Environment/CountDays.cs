using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDays : MonoBehaviour
{
   private CreateAgents createAgents;
   private int daysCounter = 0;
   private float time = 0;
   public float dayDuration = 5f;

   void Start() {
       createAgents = GetComponent<CreateAgents>();
       time = Time.time;
       StartCoroutine(DaysRoutine());
   }

   private IEnumerator DaysRoutine()
    {
        while(true)
        {
            daysCounter++;
            yield return new WaitForSeconds(dayDuration);
        }
    }
}
