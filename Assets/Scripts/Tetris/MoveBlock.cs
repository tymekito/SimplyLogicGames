using UnityEngine;

/// <summary>
/// The class for block move, and remembering blocks position.
/// Also for removing full lines.
/// </summary>

public class MoveBlock : MonoBehaviour
{
    private int points = 0;

    [SerializeField]
    private Vector3 blockRotationPoint;
    private float previousFallTime;
    private float fallInterval = 0.8f;

    private static int boardHeight = 25, boardWidth = 15;
    private static Transform[,] backgroundGrid = new Transform[boardWidth, boardHeight];

    private ColorChange colorChange;

    /// <summary>
    /// Gets the points. Primarly for function in Score Class
    /// </summary>
    /// <returns>An int points.</returns>
    public int GetPoints() { return points; }

    /// <summary>
    /// Chooses randam color at the creation of the block.
    /// </summary>
    private void Start()
    {
        colorChange = GetComponent<ColorChange>();
        colorChange.RandomColor();
    }

    /// <summary>
    /// Updates the block moves and calls different methods if needed.
    /// </summary>
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

    /// <summary>
    /// Adds the block to the grid in correct positions according to cubes position.
    /// </summary>
    void AddBlockToGrid()
    {
        foreach (Transform cube in transform)
        {
            int roundedX = Mathf.RoundToInt(cube.transform.position.x);
            int roundedY = Mathf.RoundToInt(cube.transform.position.y);

            backgroundGrid[roundedX, roundedY] = cube;
        }
    }

    /// <summary>
    /// Checks the grid for lines.
    /// </summary>
    void CheckGridForLines()
    {
        for(int i = boardHeight-1; i >= 0; i--)
        {
            if(IsLine(i))
            {
                DeleteLine(i);
                MoveLineDown(i);

                points += boardWidth;

                FindObjectOfType<SpawnBlock>().SetPoints(points);
            }
        }
    }

    /// <summary>
    /// Returns whether the line is grid is full or not
    /// </summary>
    /// <param name="i">The number of line</param>
    /// <returns>A wheather the line is full or not.</returns>
    bool IsLine(int i)
    {
        for(int j = 0; j < boardWidth; j++)
        {
            if (backgroundGrid[j, i] == null)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Deletes the line.
    /// </summary>
    /// <param name="i">The line number.</param>
    void DeleteLine(int i)
    {
        for (int j = 0; j < boardWidth; j++)
        {
            Destroy(backgroundGrid[j, i].gameObject);
            backgroundGrid[j, i] = null;
        }
    }

    /// <summary>
    /// Moves the line down.
    /// </summary>
    /// <param name="i">The line number.</param>
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

    /// <summary>
    /// Checks if block move is possible.
    /// Which means whether it collides with wall or other block
    /// </summary>
    /// <returns>An information about whether move is possible or not.</returns>
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
