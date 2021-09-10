using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] int enemyCount = 0;
    [SerializeField] float minSpawnPosition = -5f;
    [SerializeField] float maxSpawnPosition = 5f;
    [SerializeField] List<GameObject> enemiesToSpawn;

    static int createdAIEnemyCount;

    GameObject player;
    Transform playerTrans;

    int createdEnemyCount = 0;
    float spawnTimer = 0;
    float spawnDistance = 65;
    bool canSpawn = false;

    const int maxAIEnemyCount = 5;
    const float spawnInterval = .2f;

    #endregion

    private void Start()
    {
        createdAIEnemyCount = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }

    void Update()
    {
        if (PlayerController.isGameStart)
        {
            if (player != null)
            {
                Vector3 dist = transform.position - playerTrans.position;

                canSpawn = dist.z <= spawnDistance;

                if (canSpawn)
                {
                    if (createdEnemyCount < enemyCount)
                        spawn();
                }
            }
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
        int spawnEnemyInx = Random.Range(0, enemiesToSpawn.Count);
        GameObject enemyToSpawn = enemiesToSpawn[spawnEnemyInx];

        if (enemyToSpawn.GetComponent<NavMeshAgent>() != null)
        {
            if (createdAIEnemyCount >= maxAIEnemyCount)
            {
                enemiesToSpawn.RemoveAt(spawnEnemyInx);

                spawnEnemyInx = Random.Range(0, enemiesToSpawn.Count);
                enemyToSpawn = enemiesToSpawn[spawnEnemyInx];
            }
            else createdAIEnemyCount++;
        }

        float randomXPos = Random.Range(minSpawnPosition, maxSpawnPosition);

        Vector3 spawnPos = new Vector3(randomXPos, .5f, transform.position.z);

        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

        createdEnemyCount++;
    }
}
