using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField]
    private Vector3 blockRotationPoint;
    private float previousFallTime;
    private float fallInterval = 0.8f;

    private static int boardHeight = 25, boardWidth = 15;
    private static Transform[,] backgroundGrid = new Transform[boardWidth, boardHeight];

    private ColorChange colorChange;

    private void Start()
    {
        colorChange = GetComponent<ColorChange>();
        colorChange.randomColor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);

            if(!IsMovePossible())
                transform.position += new Vector3(1, 0, 0);
        }

        else if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!IsMovePossible())
                transform.position += new Vector3(-1, 0, 0);
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            transform.RotateAround(transform.TransformPoint(blockRotationPoint), new Vector3(0, 0, 1), 90.0f);

            if (!IsMovePossible())
                transform.RotateAround(transform.TransformPoint(blockRotationPoint), new Vector3(0, 0, 1), -90.0f);
        }

        if(Time.time - previousFallTime > (Input.GetKey(KeyCode.Space) ? fallInterval / 10: fallInterval))
        {
            transform.position += new Vector3(0, -1, 0);
            previousFallTime = Time.time;

            if (!IsMovePossible())
            {
                transform.position += new Vector3(0, 1, 0);

                AddBlockToGrid();
                CheckGridForLines();

                this.enabled = false;
                FindObjectOfType<SpawnBlock>().SpawnBlockInstance();
            }

        }
    }

    void AddBlockToGrid()
    {
        foreach (Transform cube in transform)
        {
            int roundedX = Mathf.RoundToInt(cube.transform.position.x);
            int roundedY = Mathf.RoundToInt(cube.transform.position.y);

            backgroundGrid[roundedX, roundedY] = cube;
        }
    }

    void CheckGridForLines()
    {
        for(int i = boardHeight-1; i >= 0; i--)
        {
            if(IsLine(i))
            {
                DeleteLine(i);
                MoveLineDown(i);
            }
        }
    }

    bool IsLine(int i)
    {
        for(int j = 0; j < boardWidth; j++)
        {
            if (backgroundGrid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < boardWidth; j++)
        {
            Destroy(backgroundGrid[j, i].gameObject);
            backgroundGrid[j, i] = null;
        }
    }

    void MoveLineDown(int i)
    {
        for(int tmpI = i; tmpI < boardHeight; tmpI++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if(backgroundGrid[j, tmpI] != null)
                {
                    backgroundGrid[j, tmpI - 1] = backgroundGrid[j, tmpI];
                    backgroundGrid[j, tmpI] = null;
                    backgroundGrid[j, tmpI - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    bool IsMovePossible()
    {
        foreach(Transform cube in transform)
        {
            int roundedX = Mathf.RoundToInt(cube.transform.position.x);
            int roundedY = Mathf.RoundToInt(cube.transform.position.y);
            
            if(roundedX < 0 || roundedX >= boardWidth || roundedY < 0 || roundedY >= boardHeight)
            {
                return false;
            }

            if (backgroundGrid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }
}
