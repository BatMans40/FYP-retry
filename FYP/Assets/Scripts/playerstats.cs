using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public GameObject player;
    public float health;
    public float maxHealth;

    // Add references to UI components
    public Slider healthslider;
    public Text healthText;
    public Text gemsValue;
    public Text coinsValue;

    public int coins;
    public int gems;

    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
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

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        healthslider.value = CalculateHealthPercentage();
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

    public void AddCurrency(CurrencyPickup currency)
    {
        if (currency.currentObject == CurrencyPickup.PickupObject.COIN)
        {
            coins += currency.pickupQuantity;
            coinsValue.text = "Coins: " + coins.ToString(); // Update the coinsValue text
        }
        else if (currency.currentObject == CurrencyPickup.PickupObject.GEM)
        {
            gems += currency.pickupQuantity;
            gemsValue.text = "Gems: " + gems.ToString(); // Update the gemsValue text
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }
}