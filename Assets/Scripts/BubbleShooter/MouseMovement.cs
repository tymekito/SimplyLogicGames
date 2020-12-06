using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The mouse movement class.
/// </summary>
public class MouseMovement : MonoBehaviour
{
    private float mouseSensivity = 400.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    /// <summary>
    /// Locks the cursor at the start
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Updates the mouse position
    /// </summary>
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);
        yRotation -= mouseX;

        transform.localRotation = Quaternion.Euler(-xRotation, yRotation, 0f);
    }
}
