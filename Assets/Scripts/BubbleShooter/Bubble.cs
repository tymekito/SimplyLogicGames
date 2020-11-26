using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;
    private Renderer bubbleRenderer;

    public void Start()
    {
        bubbleRenderer = bubble.GetComponent<Renderer>();
    }

    public GameObject SpawnBubble(Material newMaterial, Transform trans)
    {
        bubbleRenderer = bubble.GetComponent<Renderer>();
        bubbleRenderer.material = newMaterial;

        GameObject bbl = Instantiate(bubble, trans.position, Quaternion.identity);
        return gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Color color = collision.gameObject.GetComponent<Renderer>().material.color;

            if(bubbleRenderer.material.color == color)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
