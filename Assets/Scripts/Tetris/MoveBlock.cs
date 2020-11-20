using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cubes;
    private float lastAngle = 0.0f;
    private float previousTime = 0.0f;
    private float fallTime = 0.8f;

    private bool allowedToMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(allowedToMove)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1.0f, 0, 0);

                if (!possibleMove())
                    transform.position -= new Vector3(-1.0f, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(1.0f, 0, 0);

                if (!possibleMove())
                    transform.position -= new Vector3(1.0f, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                lastAngle += 90.0f;
                transform.rotation = Quaternion.Euler(0, 0, lastAngle);
            }

            if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1.0f, 0);
                previousTime = Time.time;

                if (!possibleMove())
                    transform.position -= new Vector3(0, -1.0f, 0);
            }
        }
    }

    bool possibleMove()
    {
        foreach (GameObject cube in cubes)
        {
            int roundedX = Mathf.RoundToInt(cube.transform.position.x);
            int roundedY = Mathf.RoundToInt(cube.transform.position.y);

            if (roundedX <= 0 || roundedX >= 16 || roundedY >= 20)
            {
                return false;
            }

            if (roundedY <= 1)
                allowedToMove = false;
        }

        return true;
    }
}
