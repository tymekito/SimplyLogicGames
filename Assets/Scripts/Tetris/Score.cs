using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private SpawnBlock spawnBlock;
    private int score = 0;
    private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = spawnBlock.GetPoints();

        text.text = score.ToString();
    }
}
