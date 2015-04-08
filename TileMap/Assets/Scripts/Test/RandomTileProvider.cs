using UnityEngine;
using System.Collections;

public class RandomTileProvider : TileMapDataProvider {

    public int numRows, numCols;
    public bool includeEmpty;

    protected override void LoadMap (out int rows, out int cols, out byte initialTileID) {
        rows = numRows;
        cols = numCols;
        initialTileID = Tile.NO_TILE;
    }

    protected override void BuildMapData (TileMapData data) {
        if (!tileAtlas) {
            throw new TileMapException("Need to setup tile map atlas!");
        }

        // create tiles
        Tile t0 = new Tile(0x01);
        t0.SetTileUvs(tileAtlas.UVsForTile(0));
        Tile t1 = new Tile(0x02);
        t1.SetTileUvs(tileAtlas.UVsForTile(1));
        Tile t2 = new Tile(0x03);
        t2.SetTileUvs(tileAtlas.UVsForTile(2));
        Tile t3 = new Tile(0x04);
        t3.SetTileUvs(tileAtlas.UVsForTile(3));

        data.AddMapTile(t0);
        data.AddMapTile(t1);
        data.AddMapTile(t2);
        data.AddMapTile(t3);

        // set random map tiles
        Debug.Log("Generating random tile data...");
        for (int r = 0; r < numRows; r++) {
            for (int c = 0; c < numCols; c++) {
                byte id;

                if (includeEmpty) {
                    id = (byte)Random.Range(0, 5);
                } else {
                    id = (byte)Random.Range(1, 5);
                }

                data.SetTileData(r, c, id);
            }
        }
    }
}
