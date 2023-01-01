using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TS_Actions : MonoBehaviour
{
    public int WaitScene;
    public int OptionsScene;
    public int ContinueScene;
    public static string WStatus; // For wait screen while scene loading
    public GameObject Title;
    public GameObject Buttons;
    public GameObject ButtonContinue;
    public GameObject ButtonStart;
    public GameObject UINEWGAME;
    public TMP_InputField Input_GameName;
    public int NameLimit;

    private void Start()
    {
        SkillMechanics.ResetSuicidio();


        SwitchUI(true);

        // Are there any game created?
        if (GameManager.RetrieveAllStoredGames().Count == 0)
        {
            // Hide continue button
            ButtonContinue.SetActive(false);
        }

        // if exist 3 games created
        if (GameManager.RetrieveAllStoredGames().Count == 3)
        {

            // Hide start button
            ButtonStart.SetActive(false);
        }
    }


    public void New_Game()
    {
        SwitchUI(false);

    }

    public void GoTo_NEW_Game()
    {

        if (CheckString(Input_GameName.text))
        {
            // New  game in mode normal (0)
            WStatus = "NEWGAME;0" + ";" + Input_GameName.text;
            SceneManager.LoadScene(WaitScene);
        }
        else
        {
            Input_GameName.text = "";
            Input_GameName.Select();
        }

    }


    /// <summary>
    /// Check if string is null, empy or only white spaces
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private bool CheckString(string text)
    {
        if (text != null)
        {
            string no_white = text.Replace(" ", "");

            if (string.Empty != no_white)
            {
                return true;
            }
        }

        return false;
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
        if (Input.GetKeyUp(KeyCode.Return) && UINEWGAME.activeSelf)
        {
            if (!string.IsNullOrEmpty(Input_GameName.text))
            {
                GoTo_NEW_Game();

            }

        }

        // When player press escape key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SwitchUI(true);
        }
    }

    public void Cancel()
    {
        SwitchUI(true);
    }

    public void SwitchUI(bool initial_ui)
    {
        // show title and buttons
        Buttons.SetActive(initial_ui);

        // Hide ui for title game
        UINEWGAME.SetActive(!initial_ui); // show/hide canvas of new game creation
        Input_GameName.Select(); // Select input
    }

    public void ValidateInput()
    {

        if (Input_GameName.text.Length > NameLimit)
        {
            Input_GameName.text = Input_GameName.text.Substring(0, NameLimit);
        }
    }

}
