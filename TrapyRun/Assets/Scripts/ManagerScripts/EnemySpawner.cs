using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private int enemyCount = 0;

    [SerializeField] private float minSpawnPosition = -5f;
    [SerializeField] private float maxSpawnPosition = 5f;
    [SerializeField] private float spawnDistance = 65;
    [SerializeField] private List<GameObject> enemiesToSpawn;

    private static int createdAIEnemyCount;

    private GameObject player;
    private Transform playerTrans;

    private int createdEnemyCount = 0;
    private float spawnTimer = 0;
    private bool canSpawn = false;

    private const int maxAIEnemyCount = 5;
    private const float spawnInterval = .2f;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (GameManager.currentState == GameManager.GameStates.Start)
        {
            if (player != null)
            {
                Vector3 dist = transform.position - playerTrans.position;

                canSpawn = dist.z <= spawnDistance;

                if (canSpawn)
                {
                    if (createdEnemyCount < enemyCount)
                        Spawn();
                }
            }
        }
    }

    private void InitializeVariables()
    {
        createdAIEnemyCount = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }

    private void Spawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
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
            else
            {
                createdAIEnemyCount++;
            }
        }

        float randomXPos = Random.Range(minSpawnPosition, maxSpawnPosition);

        Vector3 spawnPos = new Vector3(randomXPos, transform.position.y, transform.position.z);

        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

        createdEnemyCount++;
    }
}