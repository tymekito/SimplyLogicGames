using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    private float lastAngle = 0.0f;
    private float previousTime = 0.0f;
    private float fallTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1.0f, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1.0f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1.0f, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            lastAngle += 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, lastAngle);
        }

        if(Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
        }
    }
}
