using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckersBoardController : MonoBehaviour
{
    private Vector3 boardofset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 piceoffset = new Vector3(0.5f, 0, 0.5f);    
    private Vector2 mouseOver;
    public GameObject whitePrefab, blackPrefab, field;
    private GameObject holder;
    [HideInInspector]
    public Piece[,] fields = new Piece[8, 8];
    [HideInInspector]
    public Piece[,] pices = new Piece[8, 8];
    private GameMenager gm;
    private void Start()
    {
        GenerateBoard();
        gm = FindObjectOfType<GameMenager>();
    }
    private void Update()
    {
        MouseHandler();
    }
    private void MouseHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                mouseOver.x = (int)hit.point.x - boardofset.x;
                mouseOver.y = (int)hit.point.z - boardofset.z;
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f))
            {
                GameObject hitted = hit.transform.gameObject;
                if (hitted.tag == "Piece")
                {
                    if (gm.GetAccualPlayer() == GameMenager.Player.One && hitted.GetComponent<Piece>().isWHite == true)
                        holder = hitted;
                    else if (gm.GetAccualPlayer() == GameMenager.Player.Two && hitted.GetComponent<Piece>().isWHite == false)
                    {
                        holder = hitted;
                    }
                    else
                        holder = null;
                    // królowa
                    
                }
                else if (holder !=null && hit.transform.gameObject.tag == "Field")
                {
                    if (ValidMove(holder.transform.gameObject.GetComponent<Piece>(), hit.transform.gameObject.GetComponent<Piece>()))
                    {
                            holder.transform.position = hit.transform.position;
                            gm.ChangePlayer();
                    }
                    if (ValidAtack(holder.transform.gameObject.GetComponent<Piece>(), hit.transform.gameObject.GetComponent<Piece>()))
                    {
                            holder.transform.position = hit.transform.position;
                    }

                }
            }
        }
    }
    private void GenerateBoard()
    {
        SetFieldPiece();
        SetBlackPice();
        SetWhitePice();
    }
    private void GeneratePiece(int x, int y, GameObject objectToSpawn, bool isWHite)
    {
        GameObject go = Instantiate(objectToSpawn) as GameObject;
        go.transform.SetParent(transform);
        Piece piece = go.GetComponent<Piece>();
        piece.x = x;
        piece.y = y;
        piece.isWHite = isWHite;
        pices[x, y] = piece;
        MovePiece(piece, x, y);
    }
    private void GenerateField(int x, int y, GameObject objectToSpawn)
    {
        GameObject go = Instantiate(objectToSpawn) as GameObject;
        go.transform.SetParent(transform);
        Piece piece = go.GetComponent<Piece>();
        piece.x = x;
        piece.y = y;
        fields[x, y] = piece;
        MovePiece(piece, x, y);
    }
    private void MovePiece(Piece piece, int x, int y)
    {
        piece.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardofset + piceoffset;
    }
    private void SetBlackPice()
    {
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                if (oddRow)
                    GeneratePiece(x, y, blackPrefab, false);
                else
                    GeneratePiece(x + 1, y, blackPrefab, false);
            }
        }
    }
    private void SetWhitePice()
    {
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                if (oddRow)
                    GeneratePiece(x, y, whitePrefab, true);
                else
                    GeneratePiece(x + 1, y, whitePrefab, true);
            }
        }
    }
    private void SetFieldPiece()
    {
        for (int y = 0; y < 8; y++)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                if (oddRow)
                    GenerateField(x, y, field);
                else
                    GenerateField(x + 1, y, field);
            }
        }
    }

    /// <summary>
    /// Sprawdź czy możesz przesunąć się na pole 
    /// </summary>
    /// <param name="piece">pionek</param>
    /// <param name="field">pole docelowe</param>
    /// <returns></returns>
    private bool ValidMove(Piece piece, Piece field)
    {
        if (piece.isWHite)
        {
            if (piece.x + 1 == field.x && piece.y + 1 == field.y)
            {
                piece.x++;
                piece.y++;
                return true;
            }
            if (piece.x - 1 == field.x && piece.y + 1 == field.y)
            {
                piece.x--;
                piece.y++;
                return true;
            }
            else
                return false;
        }
        else
        {

            if ((piece.x - 1 == field.x && piece.y - 1 == field.y))
            {
                piece.x--;
                piece.y--;
                return true;
            }
            if ((piece.x + 1 == field.x && piece.y - 1 == field.y))
            {
                piece.x++;
                piece.y--;
                return true;
            }
            else
                return false;
        }
    }
    private bool ValidAtack(Piece piece, Piece field)
    {
        if (piece.isWHite)
        {
            if (piece.x + 2 == field.x && piece.y + 2 == field.y)
            {
                if (piece.checkPiece(piece.x + 1, piece.y + 1, piece.isWHite))
                {
                    piece.x += 2;
                    piece.y += 2;
                    return true;
                }
            }
            if (piece.x - 2 == field.x && piece.y + 2 == field.y)
            {
                if (piece.checkPiece(piece.x - 1, piece.y + 1, piece.isWHite))
                {
                    piece.x -= 2;
                    piece.y += 2;
                    return true;
                }
            }
            if (piece.x + 2 == field.x && piece.y - 2 == field.y)
            {
                if (piece.checkPiece(piece.x + 1, piece.y - 1, piece.isWHite))
                {
                    piece.x += 2;
                    piece.y -= 2;
                    return true;
                }
            }
            if (piece.x - 2 == field.x && piece.y - 2 == field.y)
            {
                if (piece.checkPiece(piece.x - 1, piece.y - 1, piece.isWHite))
                {
                    piece.x -= 2;
                    piece.y -= 2;
                    return true;
                }
            }
            return false;
        }
        else
        {
            if (piece.x + 2 == field.x && piece.y - 2 == field.y)
            {
                if (piece.checkPiece(piece.x + 1, piece.y - 1, piece.isWHite))
                {
                    piece.x += 2;
                    piece.y -= 2;
                    return true;
                }
            }
            if (piece.x - 2 == field.x && piece.y - 2 == field.y)
            {
                if (piece.checkPiece(piece.x - 1, piece.y - 1, piece.isWHite))
                {
                    piece.x -= 2;
                    piece.y -= 2;
                    return true;
                }
            }
            if (piece.x + 2 == field.x && piece.y + 2 == field.y)
            {
                if (piece.checkPiece(piece.x + 1, piece.y + 1, piece.isWHite))
                {
                    piece.x += 2;
                    piece.y += 2;
                    return true;
                }
            }
            if (piece.x - 2 == field.x && piece.y + 2 == field.y)
            {
                if (piece.checkPiece(piece.x - 1, piece.y + 1, piece.isWHite))
                {
                    piece.x -= 2;
                    piece.y += 2;
                    return true;
                }
            }
            return false;
        }
    }
    
}
