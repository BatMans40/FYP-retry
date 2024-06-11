using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileThrow : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float minDamage;
    public float maxDamage;
    public float projectileForce = 5f;
    public float cooldown;
    public float shootingRange = 10f;
    public LayerMask enemyLayer;
    public float rotateSpeed = 200f;
    public float homingStrength = 0.5f; // Decreased homing strength

    private float lastShotTime;

    private void Update()
    {
        if (Time.time > lastShotTime + cooldown)
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

                lastShotTime = Time.time;
                ShootAtEnemy(nearestEnemy.transform);
            }
        }
    }

    private void ShootAtEnemy(Transform target)
    {
        GameObject missile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        TestEnemyProjectile projectileScript = missile.GetComponent<TestEnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = Random.Range(minDamage, maxDamage);
        }

        HomingMissile homingMissile = missile.AddComponent<HomingMissile>();
        homingMissile.Initialize(target, projectileForce, rotateSpeed, homingStrength);
    }
}

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float rotateSpeed;
    private float homingStrength;
    private Rigidbody2D rb;

    public void Initialize(Transform target, float speed, float rotateSpeed, float homingStrength)
    {
        this.target = target;
        this.speed = speed;
        this.rotateSpeed = rotateSpeed;
        this.homingStrength = homingStrength;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        // Decrease the homing strength to make the missile hit the nearest enemy earlier
        rb.angularVelocity = -rotateAmount * rotateSpeed * homingStrength;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & LayerMask.GetMask("Enemy")) != 0)
        {
            // Put a particle effect here if needed
            Destroy(gameObject);
        }
    }
}