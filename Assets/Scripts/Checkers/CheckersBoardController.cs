using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckersBoardController : MonoBehaviour
{
    /// <summary>
    /// Board offsets
    /// </summary>
    private Vector3 boardofset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 piceoffset = new Vector3(0.5f, 0, 0.5f);
    /// <summary>
    /// Mouse x and y coordinate
    /// </summary>
    private Vector2 mouseOver;
    /// <summary>
    /// Prefabs to spawn
    /// </summary>
    public GameObject whitePrefab, blackPrefab, field;
    /// <summary>
    /// Holding piece
    /// </summary>
    private GameObject holder;
    /// <summary>
    /// Array of possible to move fields
    /// </summary>
    [HideInInspector]
    public Piece[,] fields = new Piece[8, 8];
    /// <summary>
    /// Busy fields by pieces
    /// </summary>
    [HideInInspector]
    public Piece[,] pices = new Piece[8, 8];
    private GameMenager gm;
    [HideInInspector]
    public int numberOfWhitePieces, numberOfBlackPieces;
    private void Start()
    {
        gm = FindObjectOfType<GameMenager>();
        numberOfWhitePieces = 0;
        numberOfBlackPieces = 0;
        GenerateBoard();
    }
    private void Update()
    {
        MouseHandler();
    }
    /// <summary>
    /// Mouse click and point logic
    /// </summary>
    private void MouseHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // create raycast if hitted board assign mouse coordinate
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                mouseOver.x = (int)hit.point.x - boardofset.x;
                mouseOver.y = (int)hit.point.z - boardofset.z;
            }
            // create raycast if hitted Piece test color of piece and add reference it with holder
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
                }
                // if secound mouse click hitted field test atack and move conditions
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
                    holder.transform.gameObject.GetComponent<Piece>().createQueen();
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
    /// <summary>
    /// Create piece and add it to table of pieces
    /// </summary>
    /// <param name="x">coordinate x</param>
    /// <param name="y">coordinate y</param>
    /// <param name="objectToSpawn">prefab to spawn</param>
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
    /// <summary>
    /// Generte field and add it to fields table
    /// </summary>
    /// <param name="x">coordinate x</param>
    /// <param name="y">coordinate y</param>
    /// <param name="objectToSpawn">prefab to spawn</param>
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
    /// <summary>
    /// Move piece
    /// </summary>
    /// <param name="piece">piece</param>
    /// <param name="x">destination x</param>
    /// <param name="y">destination y</param>
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
                numberOfBlackPieces++;
                if (oddRow)
                    GeneratePiece(x, y, blackPrefab, false);
                else
                    GeneratePiece(x + 1, y, blackPrefab, false);
            }
        }
    }
    /// <summary>
    /// Generate white pieces
    /// </summary>
    private void SetWhitePice()
    {
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                numberOfWhitePieces++;
                if (oddRow)
                    GeneratePiece(x, y, whitePrefab, true);
                else
                    GeneratePiece(x + 1, y, whitePrefab, true);
            }
        }
    }
    /// <summary>
    /// Generate field represent, fileds where piece can move
    /// </summary>
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
    /// Wrapper piece move
    /// </summary>
    /// <param name="piece">piece to move</param>
    /// <param name="field">detination field</param>
    /// <returns>true if moved false if cant move</returns>
    private bool ValidMove(Piece piece, Piece field)
    {
        return piece.GetComponent<Queen>().enabled ? piece.GetComponent<Queen>().MovePiece(field): piece.MovePiece(field);
    }
    /// <summary>
    /// Wrapper piece atack
    /// </summary>
    /// <param name="piece">piece to move</param>
    /// <param name="field">detination field</param>
    /// <returns>true if captured false cant capture</returns>
    private bool ValidAtack(Piece piece, Piece field)
    {
        return piece.GetComponent<Queen>().enabled ? piece.GetComponent<Queen>().AtackPiece(field) : piece.AtackPiece(field);
    }
    
}
