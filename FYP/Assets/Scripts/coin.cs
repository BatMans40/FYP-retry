using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text gemsValue;
    public Text coinsValue;
    public Text levelValue;
    public Slider coinSlider; // Reference to the Slider UI component
    private static Coin _instance;

    private int _coins;
    private int _gems;
    private int _level = 1; // Start the level at 1
    private int[] maxCoinsPerLevel = { 10, 20, 30, 40, 50 }; // Set the maximum number of coins per level
    private float sliderFillTime = 0.5f; // Time it takes to fill the slider (in seconds)

    public static Coin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Coin>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        // Initialize the slider's max value and current value
        coinSlider.maxValue = maxCoinsPerLevel[_level - 1];
        coinSlider.value = _coins % maxCoinsPerLevel[_level - 1];
    }

    public int Coins
    {
        get { return _coins; }
    }

    public int Gems
    {
        get { return _gems; }
    }

    public int Level
    {
        get { return _level; }
    }

    public void AddCurrency(CurrencyPickup currency)
    {
        if (currency.currentObject == CurrencyPickup.PickupObject.COIN)
        {
            _coins += currency.pickupQuantity;
            UpdateCoinText();
            StartCoroutine(SmoothlyFillCoinSlider(_coins - currency.pickupQuantity, _coins));

            // Check if the player has reached the maximum number of coins for the current level
            if (_coins >= maxCoinsPerLevel[_level - 1])
            {
                // Increment the level and reset the coin count
                _level++;
                _coins = _coins % maxCoinsPerLevel[_level - 1];
                UpdateCoinText();
                UpdateLevelText();
                coinSlider.maxValue = maxCoinsPerLevel[_level - 1];
                coinSlider.value = _coins;
            }
        }
        else if (currency.currentObject == CurrencyPickup.PickupObject.GEM)
        {
            _gems += currency.pickupQuantity;
            UpdateGemText();
        }
    }

    private void UpdateCoinText()
    {
        coinsValue.text = "Coins: " + _coins.ToString();
    }

    private void UpdateGemText()
    {
        gemsValue.text = "Gems: " + _gems.ToString();
    }

    private void UpdateLevelText()
    {
        levelValue.text = " " + _level.ToString();
    }

    private IEnumerator SmoothlyFillCoinSlider(float startValue, float endValue)
    {
        float elapsedTime = 0f;
        while (elapsedTime < sliderFillTime)
        {
            coinSlider.value = Mathf.Lerp(startValue % maxCoinsPerLevel[_level - 1], endValue % maxCoinsPerLevel[_level - 1], elapsedTime / sliderFillTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        coinSlider.value = endValue % maxCoinsPerLevel[_level - 1]; // Ensure the slider reaches the final value
    }
}