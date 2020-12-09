using UnityEngine;

/// <summary>
/// Queen Piece Instance
/// </summary>
public class Queen : Piece
{    
    /// <summary>
    /// Table of all pieces
    /// </summary>
    CheckersBoardController board;
    private void Start()
    {
        transform.Rotate(new Vector3(180.0f, 0.0f), Space.Self);
        Piece parent = gameObject.GetComponent<Piece>();
        x = parent.x;
        y = parent.y;
        isWHite = parent.isWHite;
        isQueen = true;
        board = FindObjectOfType<CheckersBoardController>();
        //arent.enabled = false;
    }
    /// <summary>
    /// Check if piece is hited by another piece, and destroy hitted piece, 
    /// the capture is checked against the piece table
    /// </summary>
    /// <param name="dx">hitting piece x</param>
    /// <param name="dy">hitting piece y</param>
    /// <param name="color">hitting piece color</param>
    /// <returns>true if hitted false if not hitted </returns>
    public override bool checkPiece(int dx, int dy, bool color)
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
    public override bool AtackPiece(Piece field)
    {
        int atacked;
        int max = (x - field.x);
        max = max < 0 ? max *= -1 : max *= 1;
        for (int rangeOfMove = 2; rangeOfMove <= max; rangeOfMove++)
        {
            atacked = rangeOfMove - 1;
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
            }
        }
        return false;
    }
    public override bool MovePiece(Piece field)
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
            if (x + 1 == field.x && y - 1 == field.y)
            {
                x++;
                y--;
                return true;
            }
            if (x - 1 == field.x && y - 1 == field.y)
            {
                x--;
                y--;
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
            if ((x - 1 == field.x && y + 1 == field.y))
            {
                x--;
                y++;
                return true;
            }
            if ((x + 1 == field.x && y + 1 == field.y))
            {
                x++;
                y++;
                return true;
            }
            else
                return false;
        }
    }
}
