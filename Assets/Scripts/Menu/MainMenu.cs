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
        Debug.Log(gameName);
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}


