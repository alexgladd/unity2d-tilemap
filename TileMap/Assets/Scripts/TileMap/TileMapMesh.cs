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
	
	public void BuildMapMesh (int width, int height, float tileSize = 1f) {
		// sizing
		int numTiles = width * height;
		int numVertices = numTiles * 4;
		int numTriangles = numTiles * 2 * 3;
		float halfSize = tileSize / 2f;
		
		// vert offsets
		Vector3 origin = transform.position;
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
		
		for (int h = 0; h < height; h++) {
			for (int w = 0; w < width; w++) {
				// calc tile world position
				Vector3 tilePos = new Vector3(w * tileSize, h * tileSize, 0f) + origin;
				
				// calc tile indices
				int tileIdx = (h * width) + w;
				int vStartIdx = tileIdx * 4;	// 4 verts per tile
				int tStartIdx = tileIdx * 6;	// 6 triangle verts per tile
				
				// set tile vertices
				vertices[vStartIdx]     = tilePos + v0Offset;
				vertices[vStartIdx + 1] = tilePos + v1Offset;
				vertices[vStartIdx + 2] = tilePos + v2Offset;
				vertices[vStartIdx + 3] = tilePos + v3Offset;
				
				// set tile uvs
				uvs[vStartIdx] = new Vector2(0f, 1f);
				uvs[vStartIdx + 1] = new Vector2(0.25f, 1f);
				uvs[vStartIdx + 2] = new Vector2(0.25f, 0.75f);
				uvs[vStartIdx + 3] = new Vector2(0f, 0.75f);
				
				// set tile triangles
				// t0
				triangles[tStartIdx]     = (vStartIdx);
				triangles[tStartIdx + 1] = (vStartIdx + 1);
				triangles[tStartIdx + 2] = (vStartIdx + 2);
				// t1
				triangles[tStartIdx + 3] = (vStartIdx);
				triangles[tStartIdx + 4] = (vStartIdx + 2);
				triangles[tStartIdx + 5] = (vStartIdx + 3);
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
}
