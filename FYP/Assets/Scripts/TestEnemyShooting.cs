using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;
    public float homingRate = 5f; // Rate at which the projectile adjusts its trajectory

    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (player != null)
        {
            StartCoroutine(ShootPlayer());
        }
    }

    private IEnumerator ShootPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);

            if (player != null)
            {
                // Instantiate the projectile prefab at the enemy's position
                GameObject spell = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // Set damage value
                TestEnemyProjectile projectileScript = spell.GetComponent<TestEnemyProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.damage = Random.Range(minDamage, maxDamage);
                }

                // Start the homing behavior
                StartCoroutine(HomingProjectile(spell, player.transform));

                // Destroy the projectile after a certain time (you can adjust this duration)
                Destroy(spell, 3f);
            }
        }
    }

    private IEnumerator HomingProjectile(GameObject projectile, Transform target)
    {
        while (projectile != null && target != null)
        {
            // Calculate direction towards the player
            Vector2 direction = (target.position - projectile.transform.position).normalized;

            // Apply force to the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileForce;
            }

            // Adjust the projectile's rotation to face the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Wait a short duration before adjusting the trajectory again
            yield return new WaitForSeconds(1f / homingRate);
        }
    }
}