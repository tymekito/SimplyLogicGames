using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public int x, y;
    [HideInInspector]
    public bool isWHite;
    CheckersBoardController board;
    private void Start()
    {
        board = FindObjectOfType<CheckersBoardController>();
    }
    public bool checkPiece(int dx, int dy, bool color)
    {
        foreach (Piece cell in board.pices)
        {
            if (cell != null)
                if (cell.x == dx && cell.y == dy && cell.isWHite != color)
                {
                    Destroy(cell.gameObject);
                    return true;
                }
        }
        return false;
    }
}
