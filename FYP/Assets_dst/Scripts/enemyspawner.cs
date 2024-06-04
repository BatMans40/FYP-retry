using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public float spawnRate;
    private float x, y;
    private Vector3 spawnPos;

    void Start()
    {
        StartCoroutine(SpawnTestEnemy());
    }

    IEnumerator SpawnTestEnemy()
    {
        while (true) // Infinite loop for continuous spawning
        {
            x = Random.Range(-1f, 5f); // Adjusted range to include floats
            y = Random.Range(-1f, 5f); // Adjusted range to include floats
            spawnPos.x += x;
            spawnPos.y += y;
            Instantiate(Enemies[0], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}  