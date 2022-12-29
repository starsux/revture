using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TS_Actions : MonoBehaviour
{
    public int WaitScene;
    public int OptionsScene;
    public int ContinueScene;
    public static string WStatus; // For wait screen while scene loading
    public GameObject Title;
    public GameObject Buttons;
    public GameObject UINEWGAME;
    public GameObject DofVolume;
    public TMP_InputField Input_GameName;
    public int NameLimit;

    private void Start()
    {
        SwitchUI(true);

    }


    public void New_Game()
    {
        SwitchUI(false);

    }

    public void GoTo_NEW_Game()
    {

        // New  game in mode normal (0)
        WStatus = "NEWGAME;0" + ";" + Input_GameName.text;
        SceneManager.LoadScene(WaitScene);
    }

    public void Continue_Game()
    {
        SceneManager.LoadScene(ContinueScene);
    }

    public void Game_Settings()
    {
        SceneManager.LoadScene(OptionsScene);
    }

    private void Update()
    {
        // When player press enter key
        if (Input.GetKeyUp(KeyCode.Return))
        {
            GoTo_NEW_Game();

        }

        // When player press escape key
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SwitchUI(true);
        }
    }

    public void SwitchUI(bool initial_ui)
    {
        // show title and buttons
        Title.SetActive(initial_ui);
        Buttons.SetActive(initial_ui);

        // Hide ui for title game
        UINEWGAME.SetActive(!initial_ui);
        DofVolume.SetActive(!initial_ui);
    }

    public void ValidateInput()
    {
        if(Input_GameName.text.Length > NameLimit)
        {
            Input_GameName.text = Input_GameName.text.Substring(0, NameLimit);
        }
    }
}
