using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The bubble class for its spawning and state.
/// </summary>
public class Bubble : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;
    private Renderer bubbleRenderer;

    [SerializeField]
    private BubblesWall bubblesWall;

    /// <summary>
    /// Gets bubble renderer at the start, to use for changing colors
    /// </summary>
    public void Start()
    {
        bubbleRenderer = bubble.GetComponent<Renderer>();
    }

    /// <summary>
    /// Spawns the new bubble.
    /// </summary>
    /// <param name="newMaterial">The new material color.</param>
    /// <param name="trans">The transform position.</param>
    /// <returns>A GameObject of new bubble.</returns>
    public GameObject SpawnBubble(Material newMaterial, Transform trans)
    {
        bubbleRenderer = bubble.GetComponent<Renderer>();
        bubbleRenderer.material = newMaterial;

        GameObject bbl = Instantiate(bubble, trans.position, Quaternion.identity);
        return bbl;
    }

    /// <summary>
    /// Method for bubble collision with different objects
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Color color = collision.gameObject.GetComponent<Renderer>().material.color;

            if(bubbleRenderer.material.color == color)
                bubblesWall.DestroyNeighbours(this.transform, bubbleRenderer.material.color);

            Destroy(collision.gameObject);
        }
    }
}
