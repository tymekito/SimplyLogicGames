using UnityEngine;

/// <summary>
/// Piece Instance
/// </summary>
public class Piece : MonoBehaviour
{
    /// <summary>
    /// X and Y co-ordinates
    /// </summary>
    [HideInInspector]
    public int x, y;
    /// <summary>
    /// Color
    /// </summary>
    [HideInInspector]
    public bool isWHite;
    /// <summary>
    /// Queen flag
    /// </summary>
    [HideInInspector]
    public bool isQueen;
    /// <summary>
    /// Table of all pieces
    /// </summary>
    CheckersBoardController board;
    private void Start()
    {
        board = FindObjectOfType<CheckersBoardController>();
        isQueen = false;
    }
    /// <summary>
    /// Check if piece is hited by another piece, and destroy hitted piece, 
    /// the capture is checked against the piece table
    /// </summary>
    /// <param name="dx">hitting piece x</param>
    /// <param name="dy">hitting piece y</param>
    /// <param name="color">hitting piece color</param>
    /// <returns>true if hitted false if not hitted </returns>
    public virtual bool checkPiece(int dx, int dy, bool color)
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
    /// <summary>
    /// Checking if the piece can capture another piec 
    /// </summary>
    /// <param name="field"> Instance of another piece</param>
    /// <returns>true if capture false if not capture</returns>
    public virtual bool AtackPiece(Piece field)
    {
        int rangeOfMove = 2;
        int atacked = rangeOfMove-1;
        if (isWHite)
        {
            //Capture conditions
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
    /// <summary>
    /// Eneble Queen script
    /// </summary>
    public void createQueen()
    {
        if ((isWHite && y == 7) || (!isWHite && y == 0))
            gameObject.GetComponent<Queen>().enabled = true;
    }
}
