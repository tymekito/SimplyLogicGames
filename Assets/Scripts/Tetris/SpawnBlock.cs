using UnityEngine;

/// <summary>
/// The class for block spawner.
/// </summary>

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;

    private int points = 0;

    /// <summary>
    /// Creates first block at the game start.
    /// </summary>
    void Start()
    {
        SpawnBlockInstance();
    }

    /// <summary>
    /// Spawns the random block instance.
    /// </summary>
    public void SpawnBlockInstance()
    {
        int randomBlock = Random.Range(0, blocks.Length);

        Instantiate(blocks[randomBlock], transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Sets the points.
    /// </summary>
    /// <param name="newPoints">The new points.</param>
    public void SetPoints(int newPoints)
    {
        points = newPoints;
    }

    /// <summary>
    /// Gets the points.
    /// </summary>
    /// <returns>An int value of points.</returns>
    public int GetPoints()
    {
        return points;
    }
}
