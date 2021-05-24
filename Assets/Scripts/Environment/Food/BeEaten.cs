using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class BeEaten : MonoBehaviour
{
    public float nutricionalValue = 20;
    private Vector3 startScale;
    private float originalNutricionalValue;
    private void Start() {
        startScale = transform.localScale;
        originalNutricionalValue = nutricionalValue;
    }
    private void OnTriggerStay(Collider other) {
        State otherState = other.gameObject.GetComponent<State>();
       if(otherState.hunger < 100 - nutricionalValue)
        {
            otherState.HealHunger(nutricionalValue);
            Destroy(gameObject);
        }
      /*  if(otherState.hunger < 100)
        {
            float dif = 100 - otherState.hunger;
            dif = Mathf.Clamp(dif, 0, nutricionalValue);
            otherState.HealHunger(dif);
            nutricionalValue -= dif;
            transform.localScale = Vector3.Lerp(startScale, Vector3.one * 0.1f,1 -( nutricionalValue/originalNutricionalValue));
            if(nutricionalValue <= 0)
                Destroy(gameObject);
        }*/
    }
}
