using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    // Enum to define the types of pickups (COIN or GEM)
    public enum PickupObject { COIN, GEM }

    public PickupObject currentObject; // The type of pickup (set in the Inspector)
    public int pickupQuantity; // The quantity of the pickup (set in the Inspector)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming PlayerStats is a singleton or accessible globally
            PlayerStats.playerStats.AddCurrency(this); // Add the pickup to the player's currency
            Destroy(gameObject); // Destroy the pickup object
        }
    }
}