using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerupPrefabs; // Array of powerup prefabs
    public float spawnRadius = 5f; // Radius around the player to spawn the powerup
    public float spawnDelay = 60f; // Delay in seconds before the first powerup spawns

    private Transform playerTransform;
    private List<GameObject> spawnedPowerups = new List<GameObject>();

    private void Start()
    {
        // Get the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Start the coroutine to spawn powerups
        StartCoroutine(SpawnPowerupsCoroutine());
    }

    private IEnumerator SpawnPowerupsCoroutine()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            // Randomly select a powerup to spawn
            int randomIndex = Random.Range(0, powerupPrefabs.Length);
            GameObject powerupToSpawn = powerupPrefabs[randomIndex];

            // Spawn the powerup within the player's radius
            Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
            GameObject spawnedPowerup = Instantiate(powerupToSpawn, spawnPosition, Quaternion.identity);
            spawnedPowerups.Add(spawnedPowerup);

            // Wait for the next powerup to spawn
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        // Remove any powerups that have been collected
        for (int i = spawnedPowerups.Count - 1; i >= 0; i--)
        {
            if (spawnedPowerups[i] == null)
            {
                spawnedPowerups.RemoveAt(i);
            }
        }
    }
}