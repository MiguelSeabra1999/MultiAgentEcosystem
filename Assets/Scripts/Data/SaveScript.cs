using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GameData))]

public class SaveScript : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    private GameData gameData;
    private string savePath;
    void Start()
    {
        gameData = GetComponent<GameData>();
        savePath =  "../Data/gamesave.txt";
        UnityEngine.Debug.Log(savePath);
        //GetComponent<DaysController>().gameplayEvents;
        gameplayEvents.saveDataEvent += SaveData;
    }

    public void SaveData()
    {
        UnityEngine.Debug.Log("Saving data....");
        string save = "Days Agents\n" + gameData.getAgents() + " " + gameData.getDays() + "\n";

        StreamWriter writer = new StreamWriter(savePath, true);
        writer.WriteLine(save);
        writer.Close();


        UnityEngine.Debug.Log("Data Saved");
    }

}
