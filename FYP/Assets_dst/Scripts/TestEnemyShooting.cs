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

    private GameObject player;

    private void Start()
    {
        StartCoroutine(ShootPlayer());
        player = FindObjectOfType<PlayerMovement>().gameObject;
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

                // Calculate direction towards the player
                Vector2 myPos = transform.position;
                Vector2 targetPos = player.transform.position;
                Vector2 direction = (targetPos - myPos).normalized;

                // Apply force to the projectile
                Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
                rb.velocity = direction * projectileForce;

                // Set damage value
                spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);

                // Destroy the projectile after a certain time (you can adjust this duration)
                Destroy(spell, 3f);
            }
        }
    }
}