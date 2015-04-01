using UnityEngine;
using System.Collections;

public class RandomTileProvider : TileMapDataProvider {

	public int numTiles;
	public int numRows, numCols;
	
	protected override void LoadMap (out int rows, out int cols, out byte initialTileID) {
		rows = numRows;
		cols = numCols;
		initialTileID = Tile.NO_TILE;
	}
	
	protected override void BuildMapData (TileMapData data) {
		// create tiles
		Tile t0 = new Tile(0x01);
		t0.SetTileUvs(new Vector2(0f, 1f), new Vector2(0.25f, 1f), new Vector2(0.25f, 0.75f), new Vector2(0f, 0.75f));
		Tile t1 = new Tile(0x02);
		t1.SetTileUvs(new Vector2(0.25f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 0.75f), new Vector2(0.25f, 0.75f));
		Tile t2 = new Tile(0x03);
		t2.SetTileUvs(new Vector2(0.5f, 1f), new Vector2(0.75f, 1f), new Vector2(0.75f, 0.75f), new Vector2(0.5f, 0.75f));
		Tile t3 = new Tile(0x04);
		t3.SetTileUvs(new Vector2(0.75f, 1f), new Vector2(1f, 1f), new Vector2(1f, 0.75f), new Vector2(0.75f, 0.75f));
		
		data.AddMapTile(t0);
		data.AddMapTile(t1);
		data.AddMapTile(t2);
		data.AddMapTile(t3);
		
		// set random map tiles
		Debug.Log("Generating random tile data...");
		for (int r = 0; r < numRows; r++) {
			for (int c = 0; c < numCols; c++) {
				byte id = (byte)Random.Range(1, 5);
				
				//Debug.Log("Setting tile data [" + r + ", " + c + "] = 0x" + id.ToString("X2"));
				
				data.SetTileData(r, c, id);
			}
		}
	}
}
