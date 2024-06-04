using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuePlayer : MonoBehaviour
{
    private Transform playerTransform;
    public float speed; // Speed at which the enemy will pursue the player, adjustable in Unity Inspector

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Move the enemy towards the player each frame
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;
        }
    }
}