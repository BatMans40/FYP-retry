using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> HumanEnemies = new List<GameObject>();
    public List<GameObject> BatEnemies = new List<GameObject>();
    public List<GameObject> DragonEnemies = new List<GameObject>();
    public float humanSpawnRate;
    public float batSpawnRate;
    public float dragonSpawnRate;

    private Transform playerTransform;
    private bool spawnBats = false;
    private bool spawnDragons = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnAllEnemies());
    }

    IEnumerator SpawnAllEnemies()
    {
        while (true)
        {
            if (playerTransform != null)
            {
                SpawnEnemy(HumanEnemies);
                yield return new WaitForSeconds(humanSpawnRate);

                if (Time.timeSinceLevelLoad >= 30f && !spawnBats)
                {
                    spawnBats = true;
                }

                if (Time.timeSinceLevelLoad >= 60f && !spawnDragons)
                {
                    spawnDragons = true;
                }

                if (spawnBats)
                {
                    SpawnEnemy(BatEnemies);
                    yield return new WaitForSeconds(batSpawnRate);
                }

                if (spawnDragons)
                {
                    SpawnEnemy(DragonEnemies);
                    yield return new WaitForSeconds(dragonSpawnRate);
                }
            }
            else
            {
                // If the player is not found, stop spawning
                yield break;
            }
        }
    }

    void SpawnEnemy(List<GameObject> enemyPrefabs)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector3 spawnPosition = GetRandomSpawnPositionAroundPlayer();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPositionAroundPlayer()
    {
        // Define a closer spawn range around the player
        float spawnRange = 3f; // Reduced spawn range for closer spawning
        Vector3 randomDirection = Random.insideUnitCircle.normalized * spawnRange;
        Vector3 spawnPosition = playerTransform.position + randomDirection;

        // Ensure the enemy spawns at ground level (assuming z=0 is ground level)
        spawnPosition.z = 0;

        return spawnPosition;
    }
}
