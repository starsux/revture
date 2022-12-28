using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GS_CARD : MonoBehaviour
{
    public string _gamename; // game name
    public string _GAMEID; // Game id
    public PlayableCharacters _mp_character; // most played character
    public string _time_played; // mm:ss
    public int WaitScene;

    [SerializeField] public Image[] GrayGroup;

    public TextMeshProUGUI GameNameText;
    public TextMeshProUGUI GameStatsText;
    public Image banner;

    void Start()
    {
        GameNameText.text = _gamename;
        GameStatsText.text = _time_played;
    }
    public void LaunchGame()
    {
        // Go to game of current card
        // // Set status for waitScreen (Action;Game ID)
        TS_Actions.WStatus = "LOADGAME;" + SavedGames.Cards[SavedGames.CardSelected].GetComponent<GS_CARD>()._GAMEID;
        // Go to wait screen
        SceneManager.LoadScene(WaitScene);


    }
}
