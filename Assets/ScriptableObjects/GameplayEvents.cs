using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "GameplayEvents", menuName = "MultiAgentEcosystem/GameplayEvents")]
public class GameplayEvents : ScriptableObject
{
    [HideInInspector]public event UnityAction agentDiedEvent;
    [HideInInspector]public event UnityAction saveDataEvent;
    

    public void InvokeAgentDiedEvent()
    {
        if(agentDiedEvent!=null)
        {
            agentDiedEvent.Invoke();
        }
    }

    public void InvokeSaveData()
    {
        if(saveDataEvent!=null)
        {
            saveDataEvent.Invoke();
        }
    }
}

