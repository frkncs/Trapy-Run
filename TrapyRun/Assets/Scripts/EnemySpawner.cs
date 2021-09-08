using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] int enemyCount = 0;
    [SerializeField] float minSpawnPosition = -5f;
    [SerializeField] float maxSpawnPosition = 5f;
    [SerializeField] GameObject[] enemiesToSpawn;

    Transform playerTrans;

    int createdEnemyCount = 0;
    float spawnTimer = 0;
    float spawnDistance = 70;
    bool canSpawn = false;

    const float spawnInterval = .2f;

    #endregion

    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 dist = transform.position - playerTrans.position;

        canSpawn = dist.z <= spawnDistance;

        if (canSpawn)
        {
            if (createdEnemyCount < enemyCount)
                spawn();
        }
    }

    void spawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnEnemy();
            spawnTimer = 0;
        }
    }

    void spawnEnemy()
    {
        GameObject enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        Vector3 spawnPos = new Vector3(Random.Range(minSpawnPosition, maxSpawnPosition + 1), .5f, transform.position.z);

        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

        createdEnemyCount++;
    }
}
