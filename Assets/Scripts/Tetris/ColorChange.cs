using UnityEngine;

/// <summary>
/// The color change script. It randomly chooses color for the newly spawned tetris block.
/// </summary>

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cubes;

    /// <summary>
    /// Randomly chooses the color and then sets for cubes in tetris block
    /// </summary>
    public void RandomColor()
    {
        Color color = Random.ColorHSV();
        foreach (GameObject cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", color);
        }
    }
}
