using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemyProjectile : MonoBehaviour
{
    public float damage; // Damage that the projectile will deal

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile has collided with something other than an enemy
        if (collision.CompareTag("Player"))
        {
            // If it's the player, deal damage
            PlayerStats.playerStats.DealDamage(damage);
        }
    }
}