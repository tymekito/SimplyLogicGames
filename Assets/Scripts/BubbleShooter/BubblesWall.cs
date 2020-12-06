using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesWall : MonoBehaviour
{
    private static int boardWidth = 5, boardHeight = 2, boardDepth = 3;
    private GameObject[,,] bubbles = new GameObject[boardWidth, boardHeight, boardDepth];

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
                    bubbles[i, j, k] = bbl;
                }
            }
        }
    }

    public void DestroyNeighbours(Transform bubblePosition, Color color)
    {
        int x = (int)bubblePosition.position.x;
        int y = (int)bubblePosition.position.y;
        int z = (int)bubblePosition.position.z;

        if (x > 0 && bubbles[x - 1, y, z] != null)
        {
            if (color == bubbles[x - 1, y, z].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x - 1, y, z]);
                bubbles[x - 1, y, z] = null;
            }
        }
        if (x < boardWidth - 2 && bubbles[x + 1, y, z] != null)
        {
            if (color == bubbles[x + 1, y, z].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x + 1, y, z]);
                bubbles[x + 1, y, z] = null;
            }
        }

        if (y > 0 && bubbles[x, y - 1, z] != null)
        {
            if (color == bubbles[x, y - 1, z].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x, y - 1, z]);
                bubbles[x, y - 1, z] = null;
            }
        }
        if (y < boardHeight - 2 && bubbles[x, y + 1, z] != null)
        {
            if (color == bubbles[x, y + 1, z].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x, y + 1, z]);
                bubbles[x, y + 1, z] = null;
            }
        }

        if (z > 0 && bubbles[x, y, z - 1] != null)
        {
            if (color == bubbles[x, y, z - 1].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x, y, z - 1]);
                bubbles[x, y, z - 1] = null;
            }
        }
        if (z < boardDepth - 2 && bubbles[x, y, z + 1] != null)
        {
            if (color == bubbles[x, y, z + 1].gameObject.GetComponentInChildren<Renderer>().material.color)
            {
                Destroy(bubbles[x, y, z + 1]);
                bubbles[x, y, z + 1] = null;
            }
        }

        Destroy(bubbles[x, y, z]);
        bubbles[x, y, z] = null;
    }
}
