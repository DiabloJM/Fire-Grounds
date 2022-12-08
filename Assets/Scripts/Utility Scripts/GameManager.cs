using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Public Variables
    [Header("Times of Waiting")]
    public float timeStartRound = 2.0f;
    public float timeBetweenSpawn = 2.0f;

    [Header("Spawners")]
    public Transform[] spawners;

    [Header("Enemies")]
    public GameObject enemy;

    //Private Variables
    private int enemiesToSpawn;
    private int enemiesAlive;

    private void Start()
    {
        enemiesToSpawn = 3;
        enemiesAlive = enemiesToSpawn;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeStartRound);

        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemy, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    public void NextRound()
    {
        enemiesToSpawn += 2;
        enemiesAlive = enemiesToSpawn;
        StartCoroutine(SpawnEnemies());
    }
}
