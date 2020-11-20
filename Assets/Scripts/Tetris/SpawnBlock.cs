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
        int randomBlock = Random.Range(0, blocks.Length);
        if (randomBlock == 0 || randomBlock == 3)
            Instantiate(blocks[randomBlock], (transform.position + new Vector3(0.5f, 0.5f, 0)), Quaternion.identity);
        else
            Instantiate(blocks[randomBlock], transform.position, Quaternion.identity);
    }
}
