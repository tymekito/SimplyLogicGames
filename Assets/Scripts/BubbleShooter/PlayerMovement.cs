using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 12f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
/*        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);*/
    }
}
