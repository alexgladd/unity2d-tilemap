using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(TileMapMesh))]
public class TileMap : MonoBehaviour {

    [Tooltip("Data provider for tilemap")]
    public TileMapDataProvider dataProvider;
    [Tooltip("Horizontal offset into the tilemap, in tiles")]
    public int colOffset = 0;
    [Tooltip("Vertical offset into the tilemap, in tiles")]
    public int rowOffset = 0;
    [Tooltip("Width of map to render, in tiles")]
    public int width = 1;
    [Tooltip("Height of map to render, in tiles")]
    public int height = 1;
    [Tooltip("Tile size in world units")]
    public float tileSize = 1f;
    [Tooltip("Automatically build map on Start?")]
    public bool buildOnStart = true;

    private TileMapMesh tileMesh;

    // Use this for initialization
    void Start () {
        tileMesh = GetComponent<TileMapMesh>();

        if (buildOnStart) {
            BuildMap();
        }
    }

    public void BuildMap () {
        // set material from data provider if setup
        if (dataProvider.MapAtlas()) {
            tileMesh.SetTileMaterial(dataProvider.MapAtlas().Material());
        }

        if (SanityCheck()) {
            tileMesh.BuildMapMesh(dataProvider.MapData(), colOffset, rowOffset, width, height, tileSize);
        } else {
            Debug.LogError("TileMap setup did not pass sanity check (please double-check offsets, width, and height " +
                    "against underlying tile map data)");
        }
    }

    bool SanityCheck () {
        TileMapData data = dataProvider.MapData();

        if (width > data.cols) {
            return false;
        } else if (height > data.rows) {
            return false;
        } else if (colOffset >= data.cols) {
            return false;
        } else if (rowOffset >= data.rows) {
            return false;
        } else {
            return true;
        }
    }
}
