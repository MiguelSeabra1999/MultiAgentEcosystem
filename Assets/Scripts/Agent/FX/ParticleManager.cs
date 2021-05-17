using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem hearts;
    public void PlayHearts()
    {
        hearts.Play();
    }
}
