using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class BeEaten : MonoBehaviour
{
    private float nutricionalValue = 20;
    private void OnTriggerStay(Collider other) {
        State otherState = other.gameObject.GetComponent<State>();
        if(otherState.hunger < 100)
        {
            otherState.HealHunger(nutricionalValue);
            Destroy(gameObject);
        }
    }
}
