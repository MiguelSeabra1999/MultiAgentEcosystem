using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

[CreateAssetMenu(fileName = "GameplayEvents", menuName = "MultiAgentEcosystem/GameplayEvents")]
public class GameplayEvents : ScriptableObject
{
    [HideInInspector]public event UnityAction agentDiedEvent;
    [HideInInspector]public event UnityAction saveDataEvent;
    [HideInInspector]public event UnityAction passedDayEvent;
    [HideInInspector]public List<string> genomeData = new List<string>();
    private List<List<string>> geneDataByDay = new List<List<string>>();
    private List<string> obituary = new List<string>();
    private string savePath =  "Data/genomeSave.txt";
    private string savePath2 =  "Data/obituarySave.txt";

    private void OnEnable() {
        genomeData.Clear();
        geneDataByDay.Clear();
        obituary.Clear();

        using (StreamWriter tw = new StreamWriter(savePath))
        {
            tw.Write("");
            tw.Close();
        }
        using (StreamWriter tw = new StreamWriter(savePath2))
        {
            tw.Write("");
            tw.Close();
        }
    }
    private void OnDisable() {
        
    }
    public void InvokeAgentDiedEvent(Genome genome)
    {
        StreamWriter writer = new StreamWriter(savePath2, true);
    
        writer.WriteLine(genome.GetObituary());

        writer.Close();

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
    public void InvokePassedDayEvent()
    {
        if(passedDayEvent!=null)
        {
            passedDayEvent.Invoke();
        }
        SaveGenomeData();
        geneDataByDay.Add(genomeData);
        genomeData.Clear();
    }
    public void SaveGenomeData()
    {
        StreamWriter writer = new StreamWriter(savePath, true);
        foreach(string agentGenomeData in genomeData)
        {
            writer.WriteLine(agentGenomeData);
        }

        writer.Close();
    }

}

