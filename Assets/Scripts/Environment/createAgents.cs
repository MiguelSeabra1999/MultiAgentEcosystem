using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAgents : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    public GameObject agentPrefab;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public int NumberAgents;

    void Start()
    {
        gameplayEvents.agentDiedEvent += myFunc;
        for(int i = 0; i < NumberAgents; i++) {
            float x = Random.Range(minX,maxX);
            float y = Random.Range(minY,maxY);
            GameObject obj = Instantiate(agentPrefab, new Vector3(x,0,y), Quaternion.identity);
            obj.GetComponent<AgentBehaviour>().gameplayEvents = gameplayEvents;
        }
    }
    private void myFunc()
    {
        UnityEngine.Debug.Log("agent Died");
        NumberAgents--;
    }
}
