using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckersBoardController : MonoBehaviour
{
    private Vector3 boardofset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 piceoffset = new Vector3(0.5f, 0, 0.5f);
    public GameObject whitePrefab, blackPrefab;
    private Vector2 mouseOver;
    private GameObject holder;
    public GameObject field;
    public Piece[,] fields = new Piece[8, 8];
    public Piece[,] pices = new Piece[8, 8];

    private void Start()
    {
        GenerateBoard();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                mouseOver.x = (int)hit.point.x - boardofset.x;
                mouseOver.y = (int)hit.point.z - boardofset.z;
            }
            else
            {
                mouseOver.x = -1;
                mouseOver.y = -1;
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f))
            {
                if(hit.transform.gameObject.tag=="Piece")
                {
                    holder = hit.transform.gameObject;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f))
            {
                if (hit.transform.gameObject.tag == "Field")
                {
                    if (ValidMove(holder.transform.gameObject.GetComponent<Piece>(), hit.transform.gameObject.GetComponent<Piece>()))
                    {
                        holder.transform.position = hit.transform.position;
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
        SetBoard();
        SetBlackPice();
        SetWhitePice();
    }
    private void GeneratePiece(int x, int y, GameObject objectToSpawn,bool isWHite)
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
                    GeneratePiece(x, y, blackPrefab,false);
                else
                    GeneratePiece(x + 1, y, blackPrefab,false);
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
                    GeneratePiece(x, y, whitePrefab,true);
                else
                    GeneratePiece(x + 1, y, whitePrefab,true);
            }
        }
    }
    private void SetBoard()
    {
        for (int y = 0; y < 8 ; y++)
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

            if((piece.x - 1 == field.x && piece.y - 1 == field.y))
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
               if (checkPiece(piece.x+1,piece.y+1,piece.isWHite))
               {
                    piece.x+=2;
                    piece.y+=2;
                    return true;
                }
                return false;
            }
            if (piece.x - 2 == field.x && piece.y + 2 == field.y)
            {
               if (checkPiece(piece.x - 1, piece.y + 1, piece.isWHite))
               {
                    piece.x-=2;
                    piece.y+=2;
                    return true;
               }
               return false;
            }
            else
                return false;
        }
        else
        {
            if (piece.x + 2 == field.x && piece.y - 2 == field.y)
            {
                if (checkPiece(piece.x + 1, piece.y - 1,piece.isWHite))
                {
                    piece.x += 2;
                    piece.y -= 2;
                    return true;
                }
                return false;
            }
            if (piece.x - 2 == field.x && piece.y - 2 == field.y)
            {
                if (checkPiece(piece.x - 1, piece.y - 1,piece.isWHite))
                {
                    piece.x -= 2;
                    piece.y -= 2;
                    return true;
                }
                return false;
            }
            else
                return false;
        }
    }
    private bool checkPiece(int dx , int dy, bool color)
    {
        foreach (Piece cell in pices)
        {
            if (cell != null)
                if (cell.x == dx && cell.y == dy && cell.isWHite !=color)
                {
                    Destroy(cell.gameObject);
                    return true;
                }
        }
        return false;
    }
}
