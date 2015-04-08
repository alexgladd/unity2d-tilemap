using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class TileScaleCamera : MonoBehaviour {

    [Tooltip("Tile size in pixels")]
    public int tilePixelSize = 32;

    [Tooltip("Scaling factor for camera")]
    public float scale = 2f;

    [Tooltip("Update scaling continuously?")]
    public bool continuousUpdates = true;

    private Camera tileCam;

    void Awake () {
        tileCam = GetComponent<Camera>();
    }

    void Start () {
        if (tileCam) {
            tileCam.orthographic = true;
            UpdateCameraScale();
        }
    }

    void Update () {
        if (continuousUpdates && tileCam) {
            UpdateCameraScale();
        }
    }

    void UpdateCameraScale () {
        tileCam.orthographicSize = (Screen.height / tilePixelSize) / scale;
    }
}
