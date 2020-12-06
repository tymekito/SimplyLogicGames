using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The bubble shooter score.
/// </summary>
public class BubbleScore : MonoBehaviour
{
    [SerializeField]
    private BubblesWall bubblesWall;
    private int score = 0;
    [SerializeField]
    private TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
/*        text.text = score.ToString();*/
    }

    void Update()
    {
/*        score = bubblesWall.GetPoints();

        text.text = score.ToString();*/
    }
}
