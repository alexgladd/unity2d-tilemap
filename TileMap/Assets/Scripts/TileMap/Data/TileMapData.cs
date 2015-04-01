using System;
using System.Collections.Generic;

public class TileMapData {

	public int rows { get; private set; }
	public int cols { get; private set; }

	private Dictionary<byte, Tile> mapTiles;
	private byte[] mapData;
	
	public TileMapData(int mapRows, int mapCols, byte initialTileID = Tile.NO_TILE) {
		rows = mapRows;
		cols = mapCols;
		int tileArea = mapRows * mapCols;
		
		mapData = new byte[tileArea];
		
		for (int i = 0; i < mapData.Length; i++) {
			mapData[i] = initialTileID;
		}
		
		mapTiles = new Dictionary<byte, Tile>();
	}
	
	public void AddMapTile (Tile tile) {
		try {
			mapTiles.Add(tile.ID, tile);
		} catch (ArgumentException e) {
			throw new TileMapException("TileMapData already knows about tile '" + tile.Name() + "'", e);
		}
	}
	
	public Tile GetTile (int tileIndex) {
		CheckTileIndex(tileIndex);
		
		byte tileID = mapData[tileIndex];
		
		if (tileID == Tile.NO_TILE) {
			return null;
		} else if (mapTiles.ContainsKey(tileID)) {
			return mapTiles[tileID];
		} else {
			throw new TileMapException("No Tile associated with ID " + tileID);
		}
	}
	
	public Tile GetTile (int row, int col) {
		return GetTile(TileIndex(row, col));
	}
	
	public void SetTileData (int tileIndex, byte tileID) {
		CheckTileIndex(tileIndex);
		
		mapData[tileIndex] = tileID;
	}
	
	public void SetTileData (int row, int col, byte tileID) {
		SetTileData(TileIndex(row, col), tileID);
	}
	
	public int TileIndex (int row, int col) {
		return (row * this.cols) + col;
	}
	
	void CheckTileIndex (int tileIndex) {
		if (tileIndex < 0 || tileIndex >= mapData.Length) {
			throw new TileMapException("Tile index out of range " + tileIndex +
			                           " (0-" + (mapData.Length - 1) + ")");
		}
	}
}
