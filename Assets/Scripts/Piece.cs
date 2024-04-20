using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Piece : NetworkBehaviour
{
    public Board board {get; private set; }
    public TetrominoData data {get; private set; }
    public Vector3Int position {get; private set; }
    public Vector3Int[] cells {get; private set;}


    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null) {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++) {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        this.board.Clear(this);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        } else if (Input.GetKeyDown(KeyCode.I))
        {
            Move(Vector2Int.right);
        }

        this.board.Set(this);
    }

    private bool Move(Vector2Int translation)
    {
        // calculate new position
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        // test if it’s a valid position, it should be on the game board.
        bool valid = this.board.IsValidPosition(this,newPosition);

        if (valid)
        {
            this.position = newPosition;
        }

        return valid;

    }
}
