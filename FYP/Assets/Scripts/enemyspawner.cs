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
    public float humanSpawnDuration = 60f; // Duration for human enemies to spawn
    public float batSpawnDuration = 60f; // Duration for bat enemies to spawn
    public float dragonSpawnDuration = 60f; // Duration for dragon enemies to spawn
    public float humanSpawnRadius = 7f;
    public float batSpawnRadius = 10f;
    public float dragonSpawnRadius = 15f;

    private Transform playerTransform;
    private bool spawnBats = false;
    private bool spawnDragons = false;
    private float humanSpawnStartTime;
    private float batSpawnStartTime;
    private float dragonSpawnStartTime;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnAllEnemies());
    }

    IEnumerator SpawnAllEnemies()
    {
        humanSpawnStartTime = Time.timeSinceLevelLoad;

        while (true)
        {
            if (playerTransform != null)
            {
                float elapsedTime = Time.timeSinceLevelLoad;

                // Human Enemies
                if (elapsedTime - humanSpawnStartTime < humanSpawnDuration)
                {
                    SpawnEnemy(HumanEnemies, humanSpawnRadius);
                    yield return new WaitForSeconds(humanSpawnRate);
                }

                // Bat Enemies
                if (elapsedTime >= 60f)
                {
                    if (!spawnBats)
                    {
                        spawnBats = true;
                        batSpawnStartTime = elapsedTime;
                    }

                    if (elapsedTime - batSpawnStartTime < batSpawnDuration)
                    {
                        SpawnEnemy(BatEnemies, batSpawnRadius);
                        yield return new WaitForSeconds(batSpawnRate);
                    }
                }

                // Dragon Enemies
                if (elapsedTime >= 90f)
                {
                    if (!spawnDragons)
                    {
                        spawnDragons = true;
                        dragonSpawnStartTime = elapsedTime;
                    }

                    if (elapsedTime - dragonSpawnStartTime < dragonSpawnDuration)
                    {
                        SpawnEnemy(DragonEnemies, dragonSpawnRadius);
                        yield return new WaitForSeconds(dragonSpawnRate);
                    }
                }

                // Check if all enemy spawn durations have ended
                if (elapsedTime - humanSpawnStartTime >= humanSpawnDuration &&
                    (elapsedTime - batSpawnStartTime >= batSpawnDuration || !spawnBats) &&
                    (elapsedTime - dragonSpawnStartTime >= dragonSpawnDuration || !spawnDragons))
                {
                    // Stop spawning enemies
                    yield break;
                }
            }
            else
            {
                // If the player is not found, stop spawning
                yield break;
            }
        }
    }

    void SpawnEnemy(List<GameObject> enemyPrefabs, float spawnRadius)
    {
        if (enemyPrefabs.Count == 0) return;

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector3 spawnPosition = GetRandomSpawnPositionAroundPlayer(spawnRadius);

        // Use the prefab's original rotation instead of a random one
        Quaternion spawnRotation = enemyPrefab.transform.rotation;

        Instantiate(enemyPrefab, spawnPosition, spawnRotation);
    }

    Vector3 GetRandomSpawnPositionAroundPlayer(float spawnRadius)
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + randomDirection;

        // Ensure the enemy spawns at ground level (assuming z=0 is ground level)
        spawnPosition.z = 0;

        return spawnPosition;
    }
}