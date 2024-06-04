using UnityEngine;

public class EnemyFacing : MonoBehaviour
{
    private Transform enemyTransform; // Reference to the enemy's transform
    private GameObject player; // Reference to the player GameObject
    private Vector3 originalScale; // To store the original local scale

    private void Start()
    {
        enemyTransform = transform;
        originalScale = enemyTransform.localScale; // Store the original local scale
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
    }

    private void Update()
    {
        if (player != null)
        {
            // Determine the facing direction based on the player's position
            if (player.transform.position.x < enemyTransform.position.x)
            {
                // Player is to the left, flip the enemy to face left
                enemyTransform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else if (player.transform.position.x > enemyTransform.position.x)
            {
                // Player is to the right, ensure the enemy faces right
                enemyTransform.localScale = originalScale;
            }
        }
    }
}