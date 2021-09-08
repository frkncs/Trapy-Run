using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] GameObject[] enemiesToSpawn;

    float spawnTimer = 0;
    const float spawnInterval = .2f;

	#endregion

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            if (canSpawnEnemy())
                spawnEnemy();

            spawnTimer = 0;
        }
    }

    bool canSpawnEnemy()
    {
        return Physics.Raycast(transform.position, Vector3.down, 15, LayerMask.GetMask("Ground"));
    }

    float spawnRate = 5f;

    void spawnEnemy()
    {
        GameObject enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRate, spawnRate + 1), 1, transform.position.z);

        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
    }
}
