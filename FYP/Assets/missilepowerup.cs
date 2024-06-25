using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject rotatingObjectPrefab; // Assign the rotating object prefab in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the rotating object and set it to rotate around the player
            GameObject rotatingObject = Instantiate(rotatingObjectPrefab, other.transform.position, Quaternion.identity);
            RotateAround rotateAroundScript = rotatingObject.GetComponent<RotateAround>();
            rotateAroundScript.Activate(other.transform);

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}