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
    /// <summary>
    /// Znajdz komórkę o współrzędnych x i y o innym kolorze niż pionek który próbujesz zbić
    /// </summary>
    /// <param name="dx">X zbitego pionka</param>
    /// <param name="dy">Y zbitego pionka</param>
    /// <param name="color">color zbitego pionka</param>
    /// <returns>wynik czy zbito pionka</returns>
    public virtual bool checkPiece(int dx, int dy, bool color)
    {
        foreach (Piece cell in board.pices)
        {
            if (cell != null)
                if (cell.x == dx && cell.y == dy && cell.isWHite != color)// colors are diffrent
                {
                    Destroy(cell.gameObject);
                    return true;
                }
        }
        return false;
    }
}
