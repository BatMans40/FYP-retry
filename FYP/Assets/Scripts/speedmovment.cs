using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    public float speedMultiplier = 2f; // Multiplier for the speed effect, adjustable in the Unity Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply the speed change immediately
            ChangeSpeed(other.gameObject);

            // Destroy the power-up object immediately
            Destroy(gameObject);
        }
    }

    private void ChangeSpeed(GameObject player)
    {
        // Assuming the player has a 'PlayerMovement' script with a 'speed' variable
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // Double the speed of the player
        playerMovement.speed *= speedMultiplier;
    }
}