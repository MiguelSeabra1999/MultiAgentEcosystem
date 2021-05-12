using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAgents : MonoBehaviour
{
    public GameObject agentPrefab;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public int NumberAgents;

    void Start()
    {
        for(int i = 0; i < NumberAgents; i++) {
            float x = Random.Range(minX,maxX);
            float y = Random.Range(minY,maxY);
            Instantiate(agentPrefab, new Vector3(x,0,y), Quaternion.identity);
        }
    }
}
