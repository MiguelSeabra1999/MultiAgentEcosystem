using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{

    public DaysController daysController;
    public CreateAgents createAgents;
    public SpawnFood spawnFood;
    //public CreateAgents createAgents;
    public int agents {get; set;}
    public float days {get; set;}
    public int foodCount {get; set;}

    //[SerializeField]
    private Text agentsText;
    //[SerializeField]
    private Text daysText;

    void Start() {

    }

    public void SetData(int ag, float da) {
        agents = ag; days = da;
    }

    public void ShowData() {
        agentsText.text = agents.ToString();
        daysText.text = days.ToString();
    }

    public int GetAgents() {
        agents = createAgents.NumberAgents;
        return agents;
    }

    public float GetDays() {
        days = daysController.daysCounter;
        return days;
    }
    public int GetFoodCount() {

        return GameObject.FindGameObjectsWithTag("Food").Length;
    }

}
