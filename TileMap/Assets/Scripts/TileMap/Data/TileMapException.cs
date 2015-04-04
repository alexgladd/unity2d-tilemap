using System;

public class TileMapException : Exception {
    public TileMapException () { }
    public TileMapException (string msg) : base(msg) { }
    public TileMapException (string msg, Exception cause) : base(msg, cause) { }
}
