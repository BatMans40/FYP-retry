using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    private WorldScrolling worldScrolling;

    void Start()
    {
        worldScrolling = GetComponentInParent<WorldScrolling>();
        if (worldScrolling == null)
        {
            Debug.LogError("WorldScrolling component not found in parent!");
            return;
        }

        worldScrolling.RegisterTile(this);
    }

    public void UpdatePosition(Vector2Int newTilePosition)
    {
        float tileSize = worldScrolling.tileSize;
        transform.position = new Vector3(newTilePosition.x * tileSize, newTilePosition.y * tileSize, 0);
    }

    // New method to recycle the tile
    public void RecycleTile(Vector2Int playerTilePosition)
    {
        Vector2Int tilePosition = new Vector2Int(
            Mathf.FloorToInt(transform.position.x / worldScrolling.tileSize),
            Mathf.FloorToInt(transform.position.y / worldScrolling.tileSize)
        );

        // Calculate the distance from the player to the tile in both x and y directions
        int deltaX = tilePosition.x - playerTilePosition.x;
        int deltaY = tilePosition.y - playerTilePosition.y;

        // Determine the new position for the tile if it's outside the visible area
        if (Mathf.Abs(deltaX) > 1)
        {
            int newX = playerTilePosition.x + (deltaX > 0 ? -1 : 1);
            tilePosition.x = newX;
        }
        if (Mathf.Abs(deltaY) > 1)
        {
            int newY = playerTilePosition.y + (deltaY > 0 ? -1 : 1);
            tilePosition.y = newY;
        }

        // Update the tile's position
        UpdatePosition(tilePosition);
    }
}