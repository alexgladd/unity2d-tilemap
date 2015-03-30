using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PixelScaleCamera : MonoBehaviour {

	[Tooltip("Tile size in pixels")]
	public int tilePixelSize = 32;
	
	[Tooltip("Scaling factor for camera")]
	public float scale = 2f;
	
	private Camera pixelCamera;
	
	void Awake () {
		pixelCamera = GetComponent<Camera>();
	}
	
	void Start () {
		
	}
	
	void Update () {
		if (pixelCamera) {
			pixelCamera.orthographic = true;
			pixelCamera.orthographicSize = (Screen.height / tilePixelSize) / scale;
		}
	}
}
