using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public abstract class TileMapDataProvider : MonoBehaviour {

    [Tooltip("Tile atlas settings (optional)")]
    public TileMapAtlas tileAtlas;

    private TileMapData data;

    void Awake () {
        InitMap();
    }

    void InitMap () {
        // init atlas if configured
        if (tileAtlas) {
            tileAtlas.Setup();
        }

        int numRows = 1, numCols = 1;
        byte initialID = Tile.NO_TILE;

        LoadMap(out numRows, out numCols, out initialID);

        data = new TileMapData(numRows, numCols, initialID);

        BuildMapData(data);
    }

    protected abstract void LoadMap (out int rows, out int cols, out byte initialTileID);

    protected abstract void BuildMapData (TileMapData data);

    public TileMapData MapData () {
        if (data == null) {
            Debug.Log("JIT map init");
            InitMap();
        }

        return data;
    }

    public TileMapAtlas MapAtlas () {
        return tileAtlas;
    }
}
