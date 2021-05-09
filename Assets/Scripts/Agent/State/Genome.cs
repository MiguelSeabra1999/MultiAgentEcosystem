using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RendererFXInterface))]
public class Genome : MonoBehaviour
{
    public float speed = 10;
    public float vitality = 10;
    public float starvingDamage = 0.1f;
    public float wanderRate = 0.0005f;
    public float senseRadius = 5f;
    public Color color;

    private RendererFXInterface rendererFXInterface;
    private void Awake() {
        rendererFXInterface = GetComponent<RendererFXInterface>();
        rendererFXInterface.SetColor(color);
    }

}


