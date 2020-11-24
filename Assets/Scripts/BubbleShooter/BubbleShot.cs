using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShot : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private Camera camera;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shot");

            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;

        else
            destination = ray.GetPoint(1000);

        InstatiateLateProjectile();
    }

    void InstatiateLateProjectile()
    {

    }
}
