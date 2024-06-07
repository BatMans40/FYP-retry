using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileProjectileDmg : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f; // Default damage value, can be adjusted in the Inspector
    public LayerMask enemyLayer; // LayerMask to detect enemies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is on the enemy layer
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Attempt to get the EnemyReceiveDamage component on the collision object
            EnemyReceiveDamage enemyDamageReceiver = collision.GetComponent<EnemyReceiveDamage>();
            if (enemyDamageReceiver != null)
            {
                // Call the DealDamage method and pass the damage value
                enemyDamageReceiver.DealDamage(damage);
                // Destroy the enemy object
                Destroy(collision.gameObject);
            }
            else
            {
                // Optionally, handle the case where the enemy does not have an EnemyReceiveDamage component
                Debug.LogWarning("Hit object does not have an EnemyReceiveDamage component.", collision.gameObject);
            }

            // Destroy the projectile after dealing damage
            Destroy(gameObject);
        }
    }
}