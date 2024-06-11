using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthboost : MonoBehaviour
{
    public float healthBoost = 10f; // Health that the projectile will give

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile has collided with the player
        if (collision.CompareTag("Player"))
        {
            // If it's the player, increase health
            PlayerStats.playerStats.HealCharacter(healthBoost);
        }

        // Destroy the projectile in any case to prevent it from lingering
        Destroy(gameObject);
    }
}