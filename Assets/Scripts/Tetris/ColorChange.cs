using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cubes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void randomColor()
    {
        Color color = Random.ColorHSV();
        foreach (GameObject cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", color);
        }
    }
}
