using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRuntime : MonoBehaviour
{
    public GameObject player;
    public string SpritesFolderPath;
    public GameObject PowerBarContainer;
    public Image PowerBar;
    public KeyCode KeyPause;
    public KeyCode KeyHelp;
    public KeyCode KeyMap;
    public KeyCode KeyInventory;
    public KeyCode KeyExitSettings;
    public GameObject SettingsUI;
    public GameObject HelpUI;
    public GameObject InventoryUI;
    public GameObject MiniMap;
    public GameObject InventoryIcon;
    public Dialog _dg;
    public GameObject UIPause;
    private PlayerManager _pm;
    public static bool GLOBALPAUSE = false;
    public int TitleScreenScene;
    public GameObject[] SkillSlots;
    public GameObject[] HelpSlots;

    public GameObject Icon_arantia;
    public GameObject Icon_stenpek;
    public GameObject Icon_ren;
    public GameObject Icon_pikun;

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
                    _dg.ShowDialog(GameManager.currentGame.StoryControl.Diag[0], GameManager.currentGame.StoryControl.Diag[0].Character);
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
            if (Input.GetKeyUp(KeyMap) && GameManager.currentGame.MapUnlocked)
            {
                Debug.Log("Big  map");
            }


            // Is player press KeyMap?
            if (Input.GetKeyUp(KeyInventory) && GameManager.currentGame.InventoryUnlocked)
            {
                InventoryUI.SetActive(!InventoryUI.activeSelf);
            }

            MiniMap.SetActive(GameManager.currentGame.MapUnlocked);
            InventoryIcon.SetActive(GameManager.currentGame.MapUnlocked);
        }

        PowerBarContainer.SetActive(GameManager.currentGame.SkillSlotsUnlocked > 0);

        for (int i = 0; i < SkillSlots.Length; i++)
        {
            SkillSlots[i].SetActive(i < GameManager.currentGame.SkillSlotsUnlocked);
            HelpSlots[i].SetActive(i < GameManager.currentGame.SkillSlotsUnlocked);
        }


        // Is player press Key pause?
        if (( Input.GetKeyUp("joystick 1 button 10") || Input.GetKeyUp(KeyPause)) && !SettingsUI.activeSelf)
        {
            SetPause(!GLOBALPAUSE);
        }

        // Is player press exit key for settings?
        if (Input.GetKeyUp(KeyExitSettings) && SettingsUI.activeSelf)
        {
            SettingsUI.SetActive(!SettingsUI.activeSelf);

        }
    }

    /// <summary>
    /// Switch between pause or resume
    /// Set scale of time to zero for physics
    /// </summary>
    public void SetPause(bool pause)
    {
        GLOBALPAUSE = pause;
        Time.timeScale = pause ? 0 : 1;
        UIPause.SetActive(pause);
    }

    public void GoToTitle()
    {
        SetPause(false);
        SceneManager.LoadScene(TitleScreenScene);
    }

    public void Settings()
    {
        SettingsUI.SetActive(!SettingsUI.activeSelf);
    }

}
