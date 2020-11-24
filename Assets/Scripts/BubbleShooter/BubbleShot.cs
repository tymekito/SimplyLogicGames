using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShot : MonoBehaviour
{
    private float distance = 100f;

    [SerializeField]
    private Camera camera;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out raycastHit, distance))
        {
            Debug.Log(raycastHit.transform.name);
        }

    }
}
