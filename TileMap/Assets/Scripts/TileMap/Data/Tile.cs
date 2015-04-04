using UnityEngine;

public class Tile {
    public const int NUM_UVS = 4;
    public const byte NO_TILE = 0x00;

    public byte ID { get; private set; }

    private string name;
    private Vector2[] textureUvs;

    public Tile (byte id, string name = null) {
        if (id == NO_TILE) {
            throw new TileMapException("Specified tile ID outside acceptable range (0x01-0xff)");
        }

        ID = id;

        if (name == null) {
            this.name = "Tile-0x" + ID.ToString("X2");
        } else {
            this.name = name;
        }

        textureUvs = new Vector2[NUM_UVS];
    }

    public string Name () {
        return name;
    }

    public void SetTileUvs (Vector2 uv0, Vector2 uv1, Vector2 uv2, Vector2 uv3) {
        textureUvs[0] = uv0;
        textureUvs[1] = uv1;
        textureUvs[2] = uv2;
        textureUvs[3] = uv3;
    }

    public void SetTileUvs (Vector2[] uvs) {
        for (int i = 0; i < uvs.Length && i < NUM_UVS; i++) {
            textureUvs[i] = uvs[i];
        }
    }

    public Vector2 UV (int vIndex) {
        if (vIndex >= 0 && vIndex < NUM_UVS) {
            return textureUvs[vIndex];
        } else {
            throw new TileMapException("UV index out of range " + vIndex + " (0-3)");
        }
    }

    public Vector2[] UVs () {
        return textureUvs;
    }
}
