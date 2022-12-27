using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TS_Actions : MonoBehaviour
{
    public Object WaitScene;
    public Object OptionsScene;
    public Object ContinueScene;
    public static string WStatus; // For wait screen while scene loading
    public GameObject Title;
    public GameObject Buttons;
    public GameObject UINEWGAME;
    public TMP_InputField Input_GameName;

    private void Start()
    {
        // show title and buttons
        Title.SetActive(true);
        Buttons.SetActive(true);

        // Hide ui for title game
        UINEWGAME.SetActive(false);
    }


    public void New_Game()
    {
        // Hide title and buttons
        Title.SetActive(false);
        Buttons.SetActive(false);

        // Show name game 
        UINEWGAME.SetActive(true);
    }

    public void GoTo_NEW_Game()
    {

        // New  game in mode normal (0)
        WStatus = "NEWGAME;0" + ";" + Input_GameName.text;
        SceneManager.LoadScene(WaitScene.name);
    }

    public void Continue_Game()
    {
        SceneManager.LoadScene(ContinueScene.name);
    }

    public void Game_Settings()
    {
        SceneManager.LoadScene(OptionsScene.name);
    }
}
