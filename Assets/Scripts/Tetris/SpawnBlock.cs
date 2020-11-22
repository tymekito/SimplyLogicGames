using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;

    private int points = 0;
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

        Instantiate(blocks[randomBlock], transform.position, Quaternion.identity);
    }

    public void SetPoints(int newPoints)
    {
        points = newPoints;
    }

    public int GetPoints()
    {
        return points;
    }
}
