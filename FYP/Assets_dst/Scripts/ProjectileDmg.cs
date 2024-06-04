using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDmg : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            EnemyReceiveDamage enemyDamageReceiver = collision.GetComponent<EnemyReceiveDamage>();
            if (enemyDamageReceiver != null)
            {
                enemyDamageReceiver.DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}