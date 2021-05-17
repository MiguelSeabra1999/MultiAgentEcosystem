using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeliberation : MonoBehaviour
{
    private float threshold = 60.0f; // if hunger < threshold: search for food, else; search for partner


    public bool IsSearchingFood(float hunger) {
        return (hunger <= threshold);
    }

    public bool IsSearchingPartner(float hunger) {
        return (hunger > threshold);
    }

    public float FollowToProcreate(float hunger, float dist) { // gives likelihood
        float likelihood_follow_partner;

        if (dist == 0)
            likelihood_follow_partner = 100;
        else if(hunger == 0)
            likelihood_follow_partner = 0;
        else
            likelihood_follow_partner = hunger * (1 / dist);
           
        return likelihood_follow_partner;
    }

    public float FollowToAttack(float hunger, float dist) { // gives likelihood
        float likelihood_attack;

        if (dist == 0)
            likelihood_attack = 100;
        else if(hunger == 0)
            likelihood_attack = 100;
        else
            likelihood_attack = (100 - hunger) * (1 / dist);
           
        return likelihood_attack;
    }

    public bool YesToProcreate(float hunger) {
        return hunger > threshold;
    }


}
