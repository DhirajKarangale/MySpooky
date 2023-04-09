using UnityEngine;
using UnityEngine.Tilemaps;

public class TetrisGhost : MonoBehaviour
{
    [SerializeField] Tile tile;
    [SerializeField] TetrisBoard board;
    [SerializeField] TetrisPice trackingPice;
    [SerializeField] Tilemap tilemap;

    internal Vector3Int pos;
    internal Vector3Int[] cells;

    private void Awake()
    {
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePos = cells[i] + pos;
            tilemap.SetTile(tilePos, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = trackingPice.cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int pos = trackingPice.pos;

        int current = pos.y;
        int bottem = -board.boardSize.y/2 - 1;

        board.Clear(trackingPice);

        for (int row = current; row >= bottem; row--)
        {
            pos.y = row;

            if(board.IsValidPos(trackingPice,pos))
            {
                this.pos = pos;
            }
            else
            {
                break;
            }
        }

        board.Set(trackingPice);
    }

    private void Set()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePos = cells[i] + pos;
            tilemap.SetTile(tilePos, tile);
        }
    }
}
