using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 12f;

    /// <summary>
    /// Gets the character controller at the start.
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Method for moving the player character. Commented until VR/AR version.
    /// </summary>
    void Update()
    {
/*        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);*/
    }
}
