using UnityEngine;

public class TetrisPice : MonoBehaviour
{
    [SerializeField] TetrisGM gameManager;

    internal TetrisBoard board;
    internal TetrominoData data;
    internal Vector3Int pos;
    internal Vector3Int[] cells;

    public float stepDelay = 1;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float lockTime;

    private int rotationIndex;

    private void Update()
    {
        if (gameManager.isPaussed) return;

        board.Clear(this);
        lockTime += Time.deltaTime;

        if (SwipeManager.isLeft || Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (SwipeManager.isRight || Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        else if (SwipeManager.isDown || Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
        else if (Input.GetKeyDown(KeyCode.Q) || SwipeManager.isTap)
        {
            Rotate(-1);
        }
        // else if (Input.GetKeyDown(KeyCode.E) || SwipeManager.rightTap)
        // {
        //     Rotate(1);
        // }

        if (Time.time >= stepTime) Setp();

        board.Set(this);
    }


    private bool Move(Vector2Int translation)
    {
        Vector3Int newPos = this.pos;
        newPos.x += translation.x;
        newPos.y += translation.y;

        bool isValidPos = board.IsValidPos(this, newPos);

        if (isValidPos)
        {
            this.pos = newPos;
            lockTime = 0f;
        }

        return isValidPos;
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    private void Rotate(int direction)
    {
        int originalRotationIndex = rotationIndex;
        rotationIndex = Wrap(rotationIndex + direction, 0, 4);
        ApplyRotation(direction);

        if (!TestWallKicks(rotationIndex, direction))
        {
            rotationIndex = originalRotationIndex;
            ApplyRotation(-direction);
        }
    }

    private void ApplyRotation(int direction)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3 cell = this.cells[i];
            int x, y;

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * TetrisData.RotationMatrix[0] * direction) + (cell.y * TetrisData.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * TetrisData.RotationMatrix[2] * direction) + (cell.y * TetrisData.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * TetrisData.RotationMatrix[0] * direction) + (cell.y * TetrisData.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * TetrisData.RotationMatrix[2] * direction) + (cell.y * TetrisData.RotationMatrix[3] * direction));
                    break;
            }

            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKicks(rotationIndex, rotationDirection);

        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = data.wallKicks[wallKickIndex, i];

            if (Move(translation)) return true;
        }

        return false;
    }

    private int GetWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;
        if (rotationIndex < 0) wallKickIndex--;
        return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
    }

    private void Setp()
    {
        stepTime = Time.time + stepDelay;
        Move(Vector2Int.down);
        if (lockTime >= lockDelay) Lock();
    }

    private void Lock()
    {
        board.Set(this);
        board.ClearLines();
        board.SpwanPice();
        gameManager.AddScore();
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min) return max - (min - input) % (max - min);
        else return min + (input - min) % (max - min);
    }


    public void Initialize(TetrisBoard board, Vector3Int pos, TetrominoData data)
    {
        this.board = board;
        this.pos = pos;
        this.data = data;

        rotationIndex = 0;
        stepTime = Time.time + stepDelay;
        lockTime = 0f;

        if (cells == null)
        {
            cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)data.cells[i];
        }
    }
}
