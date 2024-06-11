using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScroll : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float scrollSpeed = 1f; // Adjust this as needed

    private Vector3 initialPlayerPosition;

    void Start()
    {
        initialPlayerPosition = player.position;
    }

    void Update()
    {
        float playerDistance = player.position.x - initialPlayerPosition.x;
        Vector3 scrollOffset = new Vector3(playerDistance * scrollSpeed, 0f, 0f);
        transform.position = initialPlayerPosition - scrollOffset;
    }
}