using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    public Transform playerTransform;
    public float tileSize = 6f;
    private List<TerrainTile> registeredTiles = new List<TerrainTile>();
    private Vector2Int lastPlayerTilePosition;

    private void Update()
    {
        UpdateTiles();
    }

    public void RegisterTile(TerrainTile tile)
    {
        if (!registeredTiles.Contains(tile))
        {
            registeredTiles.Add(tile);
        }
    }

    private void UpdateTiles()
    {
        Vector2Int playerTilePosition = new Vector2Int(
            Mathf.FloorToInt(playerTransform.position.x / tileSize),
            Mathf.FloorToInt(playerTransform.position.y / tileSize)
        );

        // Check for movement in any direction
        if (lastPlayerTilePosition != playerTilePosition)
        {
            lastPlayerTilePosition = playerTilePosition;
            foreach (TerrainTile tile in registeredTiles)
            {
                // Recycle the tile based on the player's new position
                tile.RecycleTile(playerTilePosition);
            }
        }
    }
}