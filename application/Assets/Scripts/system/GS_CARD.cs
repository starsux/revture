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
    public Object WaitScene;

    [SerializeField] public Image[] GrayGroup;

    public TextMeshProUGUI GameNameText;
    public TextMeshProUGUI GameStatsText;
    public Image banner;

    void Start()
    {
        GameNameText.text = _gamename;
        GameStatsText.text = _time_played;
        banner.sprite = (Sprite)Resources.Load("Img/banner" + _mp_character + ".png");
    }

}
