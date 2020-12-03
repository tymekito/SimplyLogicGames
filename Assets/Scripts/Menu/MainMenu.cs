using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Class <c>GameSelection</c> supports game selection in main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    private string gameName;
    [SerializeField] private GameObject holder;

    /// <summary>
    /// mehod <c>PlaySelected</c> loads selected game scene
    /// </summary>
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

    /// <summary>
    /// mehod <c>Quits</c> quits the application
    /// </summary>
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}


