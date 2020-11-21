﻿using System.Collections;
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
    public Text turnText;
    private void Start()
    {
        accualPlayer = Player.One;
        DisplayTurn();
    }
    public Player GetAccualPlayer()
    {
        return accualPlayer;
    }
    public void ChangePlayer()
    {
        if (accualPlayer.Equals(Player.One))
            accualPlayer = Player.Two;
        else
            accualPlayer = Player.One;
        DisplayTurn();
    }
    public void DisplayTurn()
    {
        turnText.text = "Tura Gracza: " + accualPlayer.ToString();
    }
    public void EndGame()
    {
        turnText.text = "Koniec Gry";
        Destroy(FindObjectOfType<CheckersBoardController>(),3.0f);
    }

}
