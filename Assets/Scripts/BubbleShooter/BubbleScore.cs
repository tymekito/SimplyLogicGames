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
    private TextMeshProUGUI text;

    /// <summary>
    /// Gets the text component at the start and sets to 0.
    /// </summary>
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = score.ToString();
    }

    /// <summary>
    /// Updates the score value.
    /// </summary>
    void Update()
    {
        score = bubblesWall.GetPoints();

        text.text = score.ToString();
    }
}
