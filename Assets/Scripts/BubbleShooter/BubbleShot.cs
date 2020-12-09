using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for shooting bubbles and creating new ammunition.
/// </summary>

public class BubbleShot : MonoBehaviour
{
    private float distance = 100f;

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Color[] color;
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private GameObject sphere;
    private Renderer sphereRenderer;
    Vector3 lineOrigin;

    /// <summary>
    /// Creates first bubble at the start.
    /// </summary>
    private void Start()
    {
        sphereRenderer = sphere.GetComponent<Renderer>();
        NewBubble();
    }

    /// <summary>
    /// Updates the ray of where the player is pointing at and calls method if the fire button is pressed.
    /// </summary>
    private void Update()
    {
        lineOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, camera.transform.forward * distance, Color.green);

        if (Input.GetButtonDown("Fire1"))
        {
            sphereRenderer.enabled = false;
            Shoot();

            NewBubble();
        }
    }

    /// <summary>
    /// Method called when player is shooting. Shoots the bubble into correct direction.
    /// </summary>
    private void Shoot()
    {
        lineOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        lineOrigin.y -= 0.6f;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(20f);

        GameObject bullet = Instantiate(bubble, lineOrigin, Quaternion.identity);


        Vector3 direction = targetPoint - bullet.transform.position;

        bullet.GetComponent<Rigidbody>().velocity = direction.normalized * 25f;
    }

    /// <summary>
    /// Creates new ammuntion bubble.
    /// </summary>
    private void NewBubble()
    {
        int index = Random.Range(0, materials.Length);
        sphereRenderer.material = materials[index];
        sphereRenderer.enabled = true;

        Renderer bubbleRenderer = bubble.GetComponent<Renderer>();
        bubbleRenderer.material = materials[index];
    }
}
