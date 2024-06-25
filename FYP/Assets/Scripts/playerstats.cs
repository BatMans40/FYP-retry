using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float speed; // Variable to store speed
    public static PlayerStats playerStats;
    public GameObject player;
    public float health;
    public float maxHealth;

    // Add references to UI components
    public Slider healthSlider;
    public Text healthText;

    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats.gameObject);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        health = maxHealth;
        SetHealthUI();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
    }

    public void IncreaseSpeed(float speedBoost)
    {
        speed += speedBoost;
        // Implement any additional logic needed when speed is increased
    }

    public void HealCharacter(float healAmount)
    {
        health += healAmount;
        CheckOverheal();
        SetHealthUI();
        
    }

    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
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
            health = 0;
            Destroy(player); // Destroy the player object
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}