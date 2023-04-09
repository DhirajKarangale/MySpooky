using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

[System.Serializable]
public struct TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    internal Vector2Int[] cells;
    internal Vector2Int[,] wallKicks;

    public void Initialize()
    {
        cells = TetrisData.Cells[tetromino];
        wallKicks = TetrisData.WallKicks[tetromino];
    }
}