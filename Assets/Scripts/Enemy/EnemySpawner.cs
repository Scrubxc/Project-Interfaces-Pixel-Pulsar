using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO; //Getting the enemy prefab

    float maxSpawnRateInSeconds = 3f;

    void Start()
    {

    }

    
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        //Bottom-left point of screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        
        //Top-right point of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Instantiate enemy
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y + anEnemy.gameObject.GetComponent<Transform>().localScale.y);

        //Schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;
        if(maxSpawnRateInSeconds > 1f)
        {
            //pick a number between 1 and maxSpawnRateInSeconds
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke ("SpawnEnemy", spawnInSeconds);
    }

    //Function to increase the difficulty of the game
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    //Function to start enemy spawn
    public void ScheduleEnemySpawner()
    {
		Invoke("SpawnEnemy", maxSpawnRateInSeconds);

		//Increase spawn rate every 30 seconds
		InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}

    //Function to stop enemy spawner
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

}
