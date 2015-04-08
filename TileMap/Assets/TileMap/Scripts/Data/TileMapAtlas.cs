using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileMapAtlas {

    [Tooltip("Tile atlas material")]
    public Material tileAtlasMaterial;
    [Tooltip("Number of rows of tiles in atlas")]
    public int tileRows;
    [Tooltip("Number of columns of tiles in atlas")]
    public int tileCols;
    [Tooltip("Total number of contiguous tiles in atlas")]
    public int totalTiles;

    private float uvPerRow;
    private float uvPerCol;

    // override boolean converter on this class
    public static implicit operator bool (TileMapAtlas atlas) {
        if (atlas == null || atlas.tileAtlasMaterial == null) {
            return false;
        } else {
            return true;
        }
    }

    public void Setup () {
        if (tileRows <= 0 || tileCols <= 0) {
            throw new TileMapException("Invalid setup for tile map atlas (rows and cols must be greater than zero)");
        } else if (totalTiles <= 0) {
            Debug.LogWarning("Total tiles in atlas is set to zero!");
        }

        uvPerRow = 1f / tileRows;
        uvPerCol = 1f / tileCols;

        //Debug.Log("UV increments: row=" + uvPerRow + ", col=" + uvPerCol);
    }

    /*
     * NOTE: Tile atlas indexing is row-major, starting in the top-left corner of the texture
     */

    public Vector2[] UVsForTile (int tileIndex) {
        int row = 0, col = 0;

        // get row/col for idx
        IndexToRowCol(tileIndex, out row, out col);

        return UVsForTile(row, col);
    }

    public Vector2[] UVsForTile (int row, int col) {
        if (row < 0 || row >= tileRows) {
            throw new TileMapException("Tile atlas row out of range " + row + " (" + tileRows + " rows)");
        } else if (col < 0 || col >= tileCols) {
            throw new TileMapException("Tile atlas col out of range " + col + " (" + tileCols + " rows)");
        }

        Vector2[] uvs = new Vector2[4];

        for (int i = 0; i < 4; i++) {
            uvs[i] = UVForTileVertex(row, col, i);
        }

        return uvs;
    }

    public void IndexToRowCol (int tileIndex, out int row, out int col) {
        if (tileIndex >= totalTiles) {
            throw new TileMapException("Tile atlas index out of range " + tileIndex + "(" + totalTiles + " total tiles)");
        }

        row = tileIndex / tileCols;
        col = tileIndex % tileCols;
    }

    public Material Material () {
        return tileAtlasMaterial;
    }

    Vector2 UVForTileVertex (int row, int col, int vertIndex) {
        float uvX, uvY;

        switch (vertIndex) {
            case 0:
                uvX = 0f + (col * uvPerCol);
                uvY = 1f - (row * uvPerRow);
                break;

            case 1:
                uvX = 0f + ((col + 1) * uvPerCol);
                uvY = 1f - (row * uvPerRow);
                break;

            case 2:
                uvX = 0f + ((col + 1) * uvPerCol);
                uvY = 1f - ((row + 1) * uvPerRow);
                break;

            case 3:
                uvX = 0f + (col * uvPerCol);
                uvY = 1f - ((row + 1) * uvPerRow);
                break;

            default:
                Debug.LogError("Vertex for tile UVs out of range " + vertIndex + " (0-3)");
                uvX = 0f;
                uvY = 0f;
                break;
        }

        return new Vector2(uvX, uvY);
    }
}
