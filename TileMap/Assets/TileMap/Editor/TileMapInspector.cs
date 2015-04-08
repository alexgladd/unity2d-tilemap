using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {

    public override void OnInspectorGUI () {
        base.OnInspectorGUI();

        TileMap tMap = target as TileMap;

        // add regenerate button
        if (GUILayout.Button("Regenerate")) {
            tMap.BuildMap();
        }

        // add clear button
        if (GUILayout.Button("Clear")) {
            tMap.ClearMap();
        }
    }
}
