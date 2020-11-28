using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public int x, y;
    [HideInInspector]
    public bool isWHite;
    [HideInInspector]
    public bool isQueen;
    CheckersBoardController board;
    private void Awake()
    {
        // gameObject.GetComponent<Queen>().enabled = false;
    }
    private void Start()
    {
        board = FindObjectOfType<CheckersBoardController>();
        isQueen = false;
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
    public virtual bool MovePiece(Piece field)
    {
        if (isWHite)
        {
            if (x + 1 == field.x && y + 1 == field.y)
            {
                x++;
                y++;
                return true;
            }
            if (x - 1 == field.x && y + 1 == field.y)
            {
                x--;
                y++;
                return true;
            }
            else
                return false;
        }
        else
        {

            if ((x - 1 == field.x && y - 1 == field.y))
            {
                x--;
                y--;
                return true;
            }
            if ((x + 1 == field.x && y - 1 == field.y))
            {
                x++;
                y--;
                return true;
            }
            else
                return false;
        }
    }

    public virtual bool AtackPiece(Piece field)
    {
        int rangeOfMove = 2;
        int atacked = 1;
        if (isWHite)
        {
            if (x + rangeOfMove == field.x && y + rangeOfMove == field.y)
            {
                if (checkPiece(x + atacked, y + atacked, isWHite))
                {
                    x += rangeOfMove;
                    y += rangeOfMove;
                    board.numberOfWhitePieces--;
                    return true;
                }
            }
            if (x - rangeOfMove == field.x && y + rangeOfMove == field.y)
            {
                if (checkPiece(x - atacked, y + atacked, isWHite))
                {
                    x -= rangeOfMove;
                    y += rangeOfMove;
                    board.numberOfWhitePieces--;
                    return true;
                }
            }
            if (x + rangeOfMove == field.x && y - rangeOfMove == field.y)
            {
                if (checkPiece(x + atacked, y - atacked, isWHite))
                {
                    x += rangeOfMove;
                    y -= rangeOfMove;
                    board.numberOfWhitePieces--;
                    return true;
                }
            }
            if (x - rangeOfMove == field.x && y - rangeOfMove == field.y)
            {
                if (checkPiece(x - atacked, y - atacked, isWHite))
                {
                    x -= rangeOfMove;
                    y -= rangeOfMove;
                    board.numberOfWhitePieces--;
                    return true;
                }
            }
            return false;
        }
        else
        {
            if (x + rangeOfMove == field.x && y - rangeOfMove == field.y)
            {
                if (checkPiece(x + atacked, y - atacked, isWHite))
                {
                    x += rangeOfMove;
                    y -= rangeOfMove;
                    board.numberOfBlackPieces--;
                    return true;
                }
            }
            if (x - rangeOfMove == field.x && y - rangeOfMove == field.y)
            {
                if (checkPiece(x - atacked, y - atacked, isWHite))
                {
                    x -= rangeOfMove;
                    y -= rangeOfMove;
                    board.numberOfBlackPieces--;
                    return true;
                }
            }
            if (x + rangeOfMove == field.x && y + rangeOfMove == field.y)
            {
                if (checkPiece(x + atacked, y + atacked, isWHite))
                {
                    x += rangeOfMove;
                    y += rangeOfMove;
                    board.numberOfBlackPieces--;
                    return true;
                }
            }
            if (x - rangeOfMove == field.x && y + rangeOfMove == field.y)
            {
                if (checkPiece(x - atacked, y + atacked, isWHite))
                {
                    x -= rangeOfMove;
                    y += rangeOfMove;
                    board.numberOfBlackPieces--;
                    return true;
                }
            }
            return false;
        }
    }
    public void createQueen()
    {
        if ((isWHite && y == 7) || (!isWHite && y == 0))
            gameObject.GetComponent<Queen>().enabled = true;
    }
}
