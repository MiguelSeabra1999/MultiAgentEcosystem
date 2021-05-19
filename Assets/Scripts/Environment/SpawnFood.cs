using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnFood : MonoBehaviour
{
    public GameObject foodPrefab;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float spawnChance;
    public int initialBurst = 20;
    [Tooltip("How often we test random chance")]public float spawnChanceFrequency;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        for(int i = 0; i < initialBurst;i++)
            SpawnFoodAtRandomLocation();
    }   

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            if(UnityEngine.Random.Range(0f,1f) < spawnChance)
                SpawnFoodAtRandomLocation();
            yield return new WaitForSeconds(spawnChanceFrequency);
        }
    }

    private void SpawnFoodAtRandomLocation()
    {
        float x = UnityEngine.Random.Range(minX,maxX);
        float y = UnityEngine.Random.Range(minY,maxY);
        Instantiate(foodPrefab, new Vector3(x,0,y), Quaternion.identity);
    }
}
