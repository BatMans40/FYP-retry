using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionDuration = 2f; // Duration of the explosion effect
    public float minDamage;
    public float maxDamage;
    public float projectileForce = 5f;
    public float rotateSpeed = 200f;
    public float homingStrength = 2f; // Increased homing strength for better accuracy
    public float shootingRange = 10f;
    public LayerMask enemyLayer;

    private Transform target;
    private Rigidbody2D rb;
    private bool isActivated = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        isActivated = true;
        FindTarget();
    }

    private void Update()
    {
        if (isActivated && target == null)
        {
            FindTarget();
        }
    }

    private void FixedUpdate()
    {
        if (isActivated && target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed * homingStrength;
            rb.velocity = transform.up * projectileForce;
        }
    }

    private void FindTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, shootingRange, enemyLayer);
        if (enemiesInRange.Length > 0)
        {
            // Find the nearest enemy
            Collider2D nearestEnemy = enemiesInRange[0];
            float nearestDistance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
            for (int i = 1; i < enemiesInRange.Length; i++)
            {
                float distance = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestEnemy = enemiesInRange[i];
                    nearestDistance = distance;
                }
            }
            target = nearestEnemy.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated && ((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyReceiveDamage enemyDamageReceiver = collision.GetComponent<EnemyReceiveDamage>();
            if (enemyDamageReceiver != null)
            {
                float damage = Random.Range(minDamage, maxDamage);
                enemyDamageReceiver.DealDamage(damage);
                PlayExplosionEffect();
                Destroy(gameObject); // Destroy the missile
                Destroy(collision.gameObject); // Destroy the enemy
            }
        }
    }

    private void PlayExplosionEffect()
    {
        // Instantiate the explosion effect at the missile's position
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // Destroy the explosion effect after the specified duration
        Destroy(explosion, explosionDuration);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the shooting range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}