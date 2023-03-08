using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapIterateTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the Tilemap component
        Tilemap tilemap = GetComponent<Tilemap>();

        // Get the bounds of the Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        // Loop through each tile in the Tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++) {
            for (int y = bounds.min.y; y < bounds.max.y; y++) {
                // Get the tile at the current x,y position
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));

                // Do something with the tile
                if (tile != null) {
                    Debug.DrawLine(new Vector3(x + 0.1f, y + 0.1f, 0), new Vector3(x - 0.1f, y - 0.1f, 0));
                    Debug.DrawLine(new Vector3(x + 0.1f, y - 0.1f, 0), new Vector3(x - 0.1f, y + 0.1f, 0));
                }
            }
        }
    }
}
