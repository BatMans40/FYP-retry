using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFacing : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's transform
    private Vector3 originalScale; // To store the original local scale

    private void Start()
    {
        playerTransform = transform;
        originalScale = playerTransform.localScale; // Store the original local scale
    }

    private void Update()
    {
        // Flip the player to face left when pressing 'A'
        if (Input.GetKey(KeyCode.A))
        {
            playerTransform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        // Flip the player to face right when pressing 'D'
        else if (Input.GetKey(KeyCode.D))
        {
            playerTransform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}