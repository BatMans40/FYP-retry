using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    public float healthBoost = 10f; // Health that the power-up will give

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the power-up has collided with the player
        if (collision.CompareTag("Player"))
        {
            // If it's the player, increase health
            PlayerStats.playerStats.HealCharacter(healthBoost);

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}