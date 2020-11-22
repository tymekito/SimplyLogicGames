using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{

    private void Start()
    {
        transform.Rotate(new Vector3(180.0f, 0.0f),Space.Self);
    }
    public override bool checkPiece(int dx, int dy, bool color)
    {
        return true;   
    }
}
