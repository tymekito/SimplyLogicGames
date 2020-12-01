using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// The score class for checking the game score in MoveBlock Class and updating it on the UI.
/// </summary>

public class Score : MonoBehaviour
{
    [SerializeField]
    private SpawnBlock spawnBlock;
    private int score = 0;
    private TextMeshPro text;

    /// <summary>
    /// Gets the TextMeshPro GameObject component at the start and sets score value to 0.
    /// </summary>
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = score.ToString();
    }

    /// <summary>
    /// Updates the score.
    /// </summary>
    void Update()
    {
        score = spawnBlock.GetPoints();

        text.text = score.ToString();
    }
}
