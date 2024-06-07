using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration for the double size effect

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the DoubleSize coroutine
            StartCoroutine(DoubleSize(other.transform, duration));
          
        }
    }

    private IEnumerator DoubleSize(Transform playerTransform, float time)
    {
        
        // Store the original size
        Vector3 originalSize = playerTransform.localScale;
        // Double the size of the player
        playerTransform.localScale *= 2;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Wait for the duration time
        yield return new WaitForSeconds(time);

        // Revert to the original size
        playerTransform.localScale = originalSize;
        // Destroy the power-up GameObject immediately
        Destroy(gameObject);
    }
}