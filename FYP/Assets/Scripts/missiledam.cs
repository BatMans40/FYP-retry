using UnityEngine;

public class missileProjectileDmg : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect; // Reference to the explosion effect prefab
    public float explosionDuration = 2f; // Duration of the explosion effect

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
                // Play the explosion effect
                PlayExplosionEffect(collision.transform.position);
                // Destroy the enemy object
                Destroy(collision.gameObject);
            }
            // Destroy the missile
            Destroy(gameObject);
        }
    }

    private void PlayExplosionEffect(Vector3 position)
    {
        // Instantiate the explosion effect at the collision point
        GameObject explosion = Instantiate(explosionEffect, position, Quaternion.identity);

        // Destroy the explosion effect after the specified duration
        Destroy(explosion, explosionDuration);
    }
}