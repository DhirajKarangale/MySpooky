using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisBoard : MonoBehaviour
{
    [SerializeField] TetrisGM gameManager;
    [SerializeField] TetrisPice pice;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Vector3Int spwanPos;
    [SerializeField] internal Vector2Int boardSize = new Vector2Int(10, 20);
    [SerializeField] TetrominoData[] terrainoes;

    public RectInt bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < terrainoes.Length; i++)
        {
            terrainoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpwanPice();
    }

    private bool IsLineFull(int row)
    {
        RectInt bound = bounds;

        for (int col = bound.xMin; col < bound.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            if (!tilemap.HasTile(pos)) return false;
        }

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bound = bounds;

        for (int col = bound.xMin; col < bound.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            tilemap.SetTile(pos, null);
        }

        while (row < bound.yMax)
        {
            for (int col = bound.xMin; col < bound.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(pos);

                pos = new Vector3Int(col, row, 0);
                tilemap.SetTile(pos, above);
            }

            row++;
        }
    }

    private void GameOver()
    {
        tilemap.ClearAllTiles();
        gameManager.GameOver();
    }


    public void SpwanPice()
    {
        int random = Random.Range(0, terrainoes.Length);
        TetrominoData data = terrainoes[random];
        pice.Initialize(this, spwanPos, data);

        if (IsValidPos(pice, spwanPos)) Set(pice);
        else GameOver();
    }

    public void Set(TetrisPice pice)
    {
        for (int i = 0; i < pice.cells.Length; i++)
        {
            Vector3Int tilePos = pice.cells[i] + pice.pos;
            tilemap.SetTile(tilePos, pice.data.tile);
        }
    }

    public void Clear(TetrisPice pice)
    {
        for (int i = 0; i < pice.cells.Length; i++)
        {
            Vector3Int tilePos = pice.cells[i] + pice.pos;
            tilemap.SetTile(tilePos, null);
        }
    }

    public bool IsValidPos(TetrisPice pice, Vector3Int pos)
    {
        RectInt rectBound = bounds;

        for (int i = 0; i < pice.cells.Length; i++)
        {
            Vector3Int tilePos = pice.cells[i] + pos;

            if (!rectBound.Contains((Vector2Int)tilePos)) return false;
            if (this.tilemap.HasTile(tilePos)) return false;
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bound = bounds;
        int row = bound.yMin;

        while (row < bound.yMax)
        {
            if (IsLineFull(row)) LineClear(row);
            else row++;
        }
    }
}
