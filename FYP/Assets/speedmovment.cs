using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpspeed : MonoBehaviour
{
    public float speedMultiplier = 2f; // Multiplier for the speed effect, adjustable in the Unity Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply the speed change immediately
            ChangeSpeed(other.gameObject);
        }
    }

    private void ChangeSpeed(GameObject player)
    {
        // Assuming the player has a 'PlayerMovement' script with a 'speed' variable
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // Adjust the speed of the player by the multiplier
        playerMovement.speed *= speedMultiplier;

        // Optionally, disable the power-up GameObject's visual and physical presence
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Optionally, destroy the power-up GameObject immediately or after some time
        Destroy(gameObject);
    }
}