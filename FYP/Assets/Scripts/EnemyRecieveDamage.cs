using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public CurrencyPickup coinPickup;

    public GameObject lootDrop; // Corrected variable name

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Destroy the enemy object
        Destroy(gameObject);

        // Instantiate the CurrencyPickup object
        CurrencyPickup pickup = Instantiate(coinPickup, transform.position, Quaternion.identity);
        pickup.currentObject = CurrencyPickup.PickupObject.COIN;
        
    }



    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(lootDrop, transform.position, Quaternion.identity); // Corrected function and variable names
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}
