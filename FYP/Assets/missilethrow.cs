using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missilethrow : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;
    public float homingRate = 5f;
    public float shootingRange = 10f; // Range within which the enemy can shoot at other enemies
    public LayerMask enemyLayer; // Layer to detect enemies

    private float lastShotTime; // Time when the last shot was fired

    private void Update()
    {
        if (Time.time > lastShotTime + cooldown)
        {
            // Find all enemies within shooting range
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, shootingRange, enemyLayer);
            foreach (var enemy in enemiesInRange)
            {
                // Make sure not to target self
                if (enemy.gameObject != gameObject)
                {
                    lastShotTime = Time.time;
                    ShootAtEnemy(enemy.transform);
                    break; // Shoot at the first enemy found within range
                }
            }
        }
    }

    private void ShootAtEnemy(Transform target)
    {
        // Instantiate the projectile prefab at the current position
        GameObject spell = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set damage value
        TestEnemyProjectile projectileScript = spell.GetComponent<TestEnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = Random.Range(minDamage, maxDamage);
        }

        // Start the homing behavior
        StartCoroutine(HomingProjectile(spell, target));

        // Destroy the projectile after a certain time (you can adjust this duration)
        Destroy(spell, 3f);
    }

    private IEnumerator HomingProjectile(GameObject projectile, Transform target)
    {
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        float waveFrequency = 5f; // Frequency of the wave movement
        float waveMagnitude = 1f; // Magnitude of the wave movement
        Vector2 startPosition = projectile.transform.position;
        float time = 0f;

        while (projectile != null && target != null)
        {
            // Calculate the linear direction towards the target
            Vector2 direction = (target.position - projectile.transform.position).normalized;
            Vector2 targetVelocity = direction * projectileForce;

            // Calculate the wave movement
            float wave = Mathf.Sin(time * waveFrequency) * waveMagnitude;
            Vector2 waveMovement = new Vector2(wave, 0f);

            // Combine linear and wave movements
            rb.velocity = targetVelocity + waveMovement;

            // Rotate the projectile to face the direction of movement
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Increment time
            time += Time.deltaTime;

            yield return null;
        }
    }
}