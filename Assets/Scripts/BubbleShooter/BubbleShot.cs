using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        sphereRenderer = sphere.GetComponent<Renderer>();
        NewBubble();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            sphereRenderer.enabled = false;
            Shoot();

            NewBubble();
        }
    }

    private void Shoot()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(20f);

        GameObject bullet = Instantiate(bubble, transform.position, Quaternion.identity);

        bullet.transform.parent = gameObject.transform;


        Vector3 direction = targetPoint - transform.position;

        bullet.transform.forward = direction.normalized;

        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * 10, ForceMode.VelocityChange);
    }

    private void NewBubble()
    {
        int index = Random.Range(0, materials.Length);
        sphereRenderer.material = materials[index];
        sphereRenderer.enabled = true;

        Renderer bubbleRenderer = bubble.GetComponent<Renderer>();
        bubbleRenderer.material = materials[index];
    }
}
