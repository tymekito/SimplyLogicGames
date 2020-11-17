using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public Piece[,] pices = new Piece[8, 8];
    public GameObject whitePrefab, blackPrefab;
    private Vector3 boardofset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 piceoffset = new Vector3(0.5f, 0, 0.5f);
    private Piece selectedPice;
    private Vector2 mouseOver;
    private Vector2 startDrag;
    private Vector3 endDrag;
    void Start()
    {
        GenerateBoard();
    }
    void Update()
    {
        UpdateMouseOver();
        int x = (int)mouseOver.x;
        int y = (int)mouseOver.y;
        if (Input.GetMouseButtonDown(0))
            SelectPiece(x, y);
        if (Input.GetMouseButtonUp(0))
            TryMove((int)startDrag.x, (int)startDrag.y, x, y);
    }
    void UpdateMouseOver()
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
    }
    private void SelectPiece(int x ,int y )
    {
        if(x<0|| x>= pices.Length || y<0||y>=pices.Length)
        {
            return;
        }
        Piece piece = pices[x, y];
        if(piece != null)
        {
            selectedPice = piece;
            startDrag = mouseOver;
            Debug.Log(selectedPice.name);
        }
    }
    private void GenerateBoard()
    {
        SetBlackPice();
        SetWhitePice();
    }
    private void GeneratePiece(int x, int y, GameObject objectToSpawn)
    {
        GameObject go = Instantiate(objectToSpawn) as GameObject;
        go.transform.SetParent(transform);
        Piece piece = go.GetComponent<Piece>();
        pices[x, y] = piece;
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
                GeneratePiece(oddRow ? x : x + 1, y, blackPrefab);
            }
        }
    }
    private void SetWhitePice()
    {
        for (int y = 0; y < 3; y++)
        {
            bool addRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece(addRow ? x : x + 1, y, whitePrefab);
            }
        }
    }
    private void TryMove(int startX, int startY, int endX, int endY)
    {
        startDrag = new Vector2(startX, startY);
        endDrag = new Vector2(endX, endY);
        selectedPice = pices[startX, startY];
        MovePiece(selectedPice, endX, endY);
    }
}
