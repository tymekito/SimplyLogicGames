using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesWall : MonoBehaviour
{
    private static int boardWidth = 3, boardHeight = 3, boardDepth = 3;
    private static Transform[,,] bubbleBoard = new Transform[boardWidth, boardHeight, boardDepth];

    private Bubble bubble;
    [SerializeField]
    private Material[] materials;

    void Start()
    {
        bubble = FindObjectOfType<Bubble>();
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                for (int k = 0; k < boardDepth; k++)
                {
                    Transform pos = transform;
                    pos.position = new Vector3(i, j, k);
                    int index = Random.Range(0, materials.Length);

                    GameObject bbl = bubble.SpawnBubble(materials[index], pos);
                    bbl.transform.parent = gameObject.transform;
                    bubbleBoard[i, j, k] = pos;
                }
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(bubbleBoard[1, 0, 0].gameObject);
            bubbleBoard[1, 0, 0] = null;
        }
    }
}
