using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomItem : MonoBehaviour
{
    [Header("What and Where to Spawn")]
    public List<Transform> spawnPositionList = new List<Transform>();
    public GameObject ppSpawn, lightShardSpawn;    
    public float minProbabilitySpawnValue, maxProbabilitySpawnValue;
    [Header("Limits")]
    public float spawnLimit;
    public float setTimeToRandomise;
    float timeToRandomise;
    public bool hasRandomised = false;

    BaseCurrency baseCurrency;

    void Awake()
    {
        timeToRandomise = setTimeToRandomise;
        //sets spawnlimit to just be the amount of spawn locations in case it isnt set
        if(spawnLimit == 0)
        {
            spawnLimit = spawnPositionList.Count - (spawnPositionList.Count - 1);
        }
    }
    //spawns a random currency of random type
    public void SpawnCurrency()
    {
        if (spawnPositionList[0] == null) return;
        //randomises the chosen currency by the odds set by the min/max values
        float randomCurrency = Mathf.Round(Random.Range(minProbabilitySpawnValue, maxProbabilitySpawnValue));
        //randomises the spawn position of the currency
        float randomPosition = Mathf.Round(Random.Range(0, spawnPositionList.Count));
        //sets the spawnposition of the currency to the chosen, randomised spawnposition
        Vector3 spawnPosition = spawnPositionList[(int)randomPosition].transform.position;
        //print(spawnPosition);

        //checks to see if the amount of places that can be spawned is more than the limit
        //if yes, then spawn, otherwise no
        if (spawnPositionList.Count > spawnLimit)
        {
            hasRandomised = false;
            //sets this such that PP will always have a low probability of spawning
            if (randomCurrency == minProbabilitySpawnValue)
            {
                Instantiate(ppSpawn, spawnPosition, Quaternion.identity);
                baseCurrency = FindObjectOfType<BaseCurrency>();
                baseCurrency.SetSpawnPosition(spawnPositionList[(int)randomPosition]);
                spawnPositionList.Remove(spawnPositionList[(int)randomPosition]);                               
            }
            else
            {
                Instantiate(lightShardSpawn, spawnPosition, Quaternion.identity);
                baseCurrency = FindObjectOfType<BaseCurrency>();
                baseCurrency.SetSpawnPosition(spawnPositionList[(int)randomPosition]);
                spawnPositionList.Remove(spawnPositionList[(int)randomPosition]);
            }
        }
        else
        {
            hasRandomised = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRandomised)
        {
            timeToRandomise -= Time.deltaTime;
        }
        if (timeToRandomise < 0 && !hasRandomised)
        {
            SpawnCurrency();
            timeToRandomise = setTimeToRandomise;
        }
    }
}
