using UnityEngine;
using System.Collections;

public abstract class TileMapDataProvider : MonoBehaviour {

	private TileMapData data;
	
	void Awake () {
		int numRows = 1, numCols = 1;
		byte initialID = Tile.NO_TILE;
		
		LoadMap(out numRows, out numCols, out initialID);
		
		data = new TileMapData(numRows, numCols, initialID);
		
		BuildMapData(data);
	}
	
	protected abstract void LoadMap (out int rows, out int cols, out byte initialTileID);
	
	protected abstract void BuildMapData (TileMapData data);
	
	public TileMapData MapData () {
		return data;
	}
}
