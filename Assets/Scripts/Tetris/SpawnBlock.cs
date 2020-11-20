using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBlockInstance();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBlockInstance()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
}
