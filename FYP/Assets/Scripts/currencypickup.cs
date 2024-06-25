using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public enum PickupObject { COIN, GEM }
    public PickupObject currentObject;
    public int pickupQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Coin.Instance.AddCurrency(this);
            gameObject.SetActive(false); // Deactivate for object pooling
        }
    }
}