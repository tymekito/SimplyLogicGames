using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string gameName;
    [SerializeField] private GameObject holder;
    
    public void PlaySelected()
    {
        gameName = GameSelection.gameName;
        Debug.Log(gameName.ToString());
        if(gameName == "Tetris (UnityEngine.GameObject)")
            SceneManager.LoadScene("Tetris");
        if (gameName == "Checkers (UnityEngine.GameObject)")
            SceneManager.LoadScene("Checkers");
        if (gameName == "BubbleShooter (UnityEngine.GameObject)")
            SceneManager.LoadScene("BubbleShooter");
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}


