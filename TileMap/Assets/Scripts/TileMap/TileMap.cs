using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(TileMapMesh))]
public class TileMap : MonoBehaviour {
	
	[Tooltip("Map width in tiles")]
	public int width = 1;
	[Tooltip("Map height in tiles")]
	public int height = 1;
	[Tooltip("Tile size in world units")]
	public float tileSize = 1f;
	
	private TileMapMesh tileMesh;
	
	// Use this for initialization
	void Start () {
		tileMesh = GetComponent<TileMapMesh>();
		
		BuildMap();
	}
	
	public void BuildMap () {
		tileMesh.BuildMapMesh(width, height, tileSize);
	}
}
