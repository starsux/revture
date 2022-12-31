using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRuntime : MonoBehaviour
{
    public GameObject player;
    public string SpritesFolderPath;
    public Image PowerBar;
    public KeyCode KeyPause;
    public KeyCode KeyHelp;
    public KeyCode KeyMap;
    public GameObject HelpUI;
    public Dialog _dg;
    public GameObject UIPause;
    private PlayerManager _pm;
    public static bool GLOBALPAUSE = false;
    public int TitleScreenScene;

    // Start is called before the first frame update
    void Start()
    {
        _pm = player.GetComponent<PlayerManager>();
        if (!_pm.TestMode)
        {
            // Set all playerparameters
            player.transform.position = GameManager.currentGame.PlayerPosition;
        }

    }

    private void Update()
    {
        if (!GLOBALPAUSE)
        {
            if (!_pm.TestMode)
            {
                // Dialog
                if (!GameManager.currentGame.StoryControl.Diag[0].Done)
                {
                    _dg.ShowDialog(GameManager.currentGame.StoryControl.Diag[0].ToString(), GameManager.currentGame.StoryControl.Diag[0].Character);
                }
            }


            // Change color of bar
            PowerBar.color = player.GetComponent<PlayerManager>().CurrentColor;
            PowerBar.material.SetVector("glow_color", player.GetComponent<PlayerManager>().CurrentColor);
            PowerBar.material.color = player.GetComponent<PlayerManager>().CurrentColor;

            // Is player press KeyHelp?
            if (Input.GetKeyUp(KeyHelp))
            {
                // Hide/show help
                HelpUI.SetActive(!HelpUI.activeSelf);
            }

            // Is player press KeyMap?
            if (Input.GetKeyUp(KeyMap))
            {
                Debug.Log("Big  map");
            }
        }


        // Is player press Key pause?
        if (Input.GetKeyUp(KeyPause))
        {
            Pause();
        }
    }

    public void Pause()
    {
        GLOBALPAUSE = !GLOBALPAUSE;
        Time.timeScale = GLOBALPAUSE ? 0 : 1;
        UIPause.SetActive(GLOBALPAUSE);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(TitleScreenScene);
    }

    public void Settings()
    {
        Debug.Log("Show settings game ui/scene");
    }

}
