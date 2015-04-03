using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class TileMapMesh : MonoBehaviour {

	private MeshRenderer meshRenderer;
	private MeshFilter meshFilter;
	private MeshCollider meshCollider;
	
	void Awake () {
		meshRenderer = GetComponent<MeshRenderer>();
		meshFilter = GetComponent<MeshFilter>();
		meshCollider = GetComponent<MeshCollider>();
	}
	
	public void BuildMapMesh (TileMapData mapData, float tileSize = 1f) {
		BuildMapMesh(mapData, 0, 0, mapData.cols, mapData.rows, tileSize);
	}
	
	public void BuildMapMesh (TileMapData mapData,
			int colOffset, int rowOffset, int width, int height, float tileSize) {
			
		// purge existing
		CleanMesh();
		
		// count number of actual tiles
		int numTiles = 0;
		for (int r = rowOffset; r < (rowOffset + height); r++) {
			for (int c = colOffset; c < (colOffset + width); c++) {
				if (mapData.GetTile(r, c) != null) {
					numTiles++;
				}
			}
		}
	
		// sizing
		int numVertices = numTiles * 4;
		int numTriangles = numTiles * 2 * 3;
		float halfSize = tileSize / 2f;
		
		// vert offsets
		Vector3 v0Offset = new Vector3(-halfSize, halfSize, 0f);
		Vector3 v1Offset = new Vector3(halfSize, halfSize, 0f);
		Vector3 v2Offset = new Vector3(halfSize, -halfSize, 0f);
		Vector3 v3Offset = new Vector3(-halfSize, -halfSize, 0f);
		
		// create vertices and triangles for each tile
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uvs = new Vector2[numVertices];
		int[] triangles = new int[numTriangles];
		
		/*  Tile mesh setup per tile:
		 *
		 *  v0             v1
		 *    +-----------+
		 *    | \         |
		 *    |   \   T0  |
		 *    |     X     |
		 *    |  T1   \   |
		 *    |         \ |
		 *    +-----------+
		 *  v3             v2
		 *
		 *  X = tile world position (centered)
		 *  Triangle 0 = [v0, v1, v2]
		 *  Triangle 1 = [v0, v2, v3]
		 */
		
		int curVertIdx = 0;
		int curTriIdx = 0;
		
		// start at 0,0 building up rows and columns of tiles from our current
		// transform origin
		for (int r = 0; r < height; r++) {
			for (int c = 0; c < width; c++) {
				// get tile index
				int tileIdx = mapData.TileIndex(r + rowOffset, c + colOffset);
				// get tile
				Tile t = mapData.GetTile(tileIdx);
				
				// only render mesh and texture here if there's actually a tile
				if (t != null) {
					// calc tile world position
					Vector3 tilePos = new Vector3(c * tileSize, r * tileSize, 0f);
					
					// set tile vertices
					vertices[curVertIdx]     = tilePos + v0Offset;
					vertices[curVertIdx + 1] = tilePos + v1Offset;
					vertices[curVertIdx + 2] = tilePos + v2Offset;
					vertices[curVertIdx + 3] = tilePos + v3Offset;
					
					// set tile uvs
					uvs[curVertIdx] = t.UV(0);
					uvs[curVertIdx + 1] = t.UV(1);
					uvs[curVertIdx + 2] = t.UV(2);
					uvs[curVertIdx + 3] = t.UV(3);
					
					// set tile triangles
					// t0
					triangles[curTriIdx]     = (curVertIdx);
					triangles[curTriIdx + 1] = (curVertIdx + 1);
					triangles[curTriIdx + 2] = (curVertIdx + 2);
					// t1
					triangles[curTriIdx + 3] = (curVertIdx);
					triangles[curTriIdx + 4] = (curVertIdx + 2);
					triangles[curTriIdx + 5] = (curVertIdx + 3);
					
					curVertIdx += 4;
					curTriIdx += 6;
				}
			}
		}
		
		// all normals are the same
		for (int i = 0; i < numVertices; i++) {
			normals[i] = Vector3.back;
		}
		
		// create the Mesh
		Mesh tileMesh = new Mesh();
		tileMesh.vertices = vertices;
		tileMesh.triangles = triangles;
		tileMesh.uv = uvs;
		tileMesh.normals = normals;
		
		// assign the mesh
		meshFilter.mesh = tileMesh;
	}
	
	void CleanMesh () {
		if (meshFilter.sharedMesh) {
			Debug.Log("Clearing mesh filter");
			meshFilter.sharedMesh.Clear();
		}
	}
}
