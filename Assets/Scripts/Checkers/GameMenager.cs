using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour
{
    [HideInInspector]
    public enum Player
    {
        One,
        Two
    }
    private Player accualPlayer;
    [HideInInspector]
    public Text turnText;
    [SerializeField]
    private CheckersBoardController boardPrefab;
    private CheckersBoardController board;
    private void Start()
    {
        this.SetupGame();
    }
    public Player GetAccualPlayer()
    {
        return accualPlayer;
    }
    /// <summary>
    /// Switch enum value
    /// </summary>
    public void ChangePlayer()
    {
        if (!EndGame())
        {
            if (accualPlayer.Equals(Player.One))
                accualPlayer = Player.Two;
            else
                accualPlayer = Player.One;
            DisplayTurn();
        }

    }
    /// <summary>
    /// Display text in UI
    /// </summary>
    public void DisplayTurn()
    {
        turnText.text = "Tura Gracza: " + accualPlayer.ToString();
    }
    /// <summary>
    /// Destroy board if one of pieces not exits
    /// </summary>
    /// <returns>true if no pieces one of colors false if pieces exits on board</returns>
    public bool EndGame()
    {
        if (board.numberOfBlackPieces == 0 || board.numberOfWhitePieces == 0)
        {
            turnText.text = "Wygrał gracz:" + accualPlayer.ToString();
            Destroy(board.gameObject, 3.0f);
            return true;
        }
        else
            return false;
    }
    public void SetupGame()
    {
        board = Instantiate(boardPrefab, gameObject.transform.position, Quaternion.identity);
        accualPlayer = Player.One;
        DisplayTurn();
    }
    public void NewGame()
    {
        Destroy(board.gameObject);
        SetupGame();
    }

}
