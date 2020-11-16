using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>Class <c>GameSelection</c> supports game selection in main menu
public class GameSelection : MonoBehaviour
{
    private int currentGame;
    private Button leftButton;
    private Button rightButton;
    public static string gameName;
    //{

    //    get { return gameName; }
    //    set { gameName = value; }
    //} // { get => gameName; set => gameName = value; }
    // Start is called before the first frame update
    void Start()
    {
        SelectGame(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>method <c>SelectGame</c> set active selected game</summary>
    private void SelectGame(int id)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == id);
        }
        gameName = transform.GetChild(currentGame).gameObject.ToString();
    }

    /// <summary>method <c>ChcangeGame</c> choose suitable game index</summary>
    public void ChangeGame(int change)
    {
        if (currentGame == transform.childCount - 1 && change > 0)
        {
            currentGame = 0;
        }
        else if (currentGame == 0 && change < 0)
        {
            currentGame = transform.childCount - 1;
        }
        else
        {
            currentGame += change;
        }

        SelectGame(currentGame);
    }
}
