using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public Piece[,] pices = new Piece[8, 8];
    public GameObject whitePrefab, blackPrefab;
    private Vector3 boardofset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 piceoffset = new Vector3(0.5f, 0, 0.5f);
    void Start()
    {
        GenerateBoard();
    }
    void Update()
    {

    }
    private void GenerateBoard()
    {
        SetBlackPice();
        SetWhitePice();
    }
    private void GeneratePiece(int x, int y,GameObject objectToSpawn)
    {
        GameObject go = Instantiate(objectToSpawn) as GameObject;
        go.transform.SetParent(transform);
        Piece piece = go.GetComponent<Piece>();
        pices[x, y] = piece;
        MovePiece(piece, x, y);
    }
    private void MovePiece(Piece piece, int x, int y)
    {
        piece.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardofset+ piceoffset;
    }
    private void SetBlackPice()
    {
        for (int y=7;y>4; y--)
        {
            bool oddRow = y % 2 == 0;
            for (int x=0;x<8; x+=2)
            {
                GeneratePiece(oddRow ? x : x + 1, y,blackPrefab);
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
                GeneratePiece(addRow ? x : x + 1, y,whitePrefab) ;
            }
        }
    }
}
