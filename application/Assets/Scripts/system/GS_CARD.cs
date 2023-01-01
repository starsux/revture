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
    public SavedGames _mng;


    [SerializeField] public Image[] GrayGroup;

    public TextMeshProUGUI GameNameText;
    public TextMeshProUGUI GameStatsText;
    public Image banner;

    public float IncreasingFactor = 1;
    public float DecreaseFactor = 1;

    private void Awake()
    {
        GameObject[] mangs = GameObject.FindGameObjectsWithTag("MANAGER");

        foreach (var m in mangs)
        {
            if (m.TryGetComponent(out _mng)) break;
        }
    }


    void Start()
    {
        GameNameText.text = _gamename;
        GameStatsText.text = _time_played;
    }

    public void Increase()
    {
        // Increase size of selected card
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(IncreasingFactor, IncreasingFactor, IncreasingFactor);
        _mng.ApplyGrayScale(this.gameObject, false);
    }

    public void Normal()
    {
        // Return to normal state
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(DecreaseFactor, DecreaseFactor, DecreaseFactor);
        _mng.ApplyGrayScale(this.gameObject, true);
    }

    // Check if position is in range of sqaure
    private bool InRangeCenterSquare(Vector3 position, int v)
    {
        if (position.x <= v && position.x >= v * -1)
        {
            if (position.y <= v && position.y >= v * -1)
            {
                return true;
            }
        }

        return false;
    }

    private void OnMouseUp()
    {
        LaunchGame();
    }

    public void LaunchGame()
    {
        // Go to game of current card
        // // Set status for waitScreen (Action;Game ID)
        TS_Actions.WStatus = "LOADGAME;" + SavedGames.Cards[SavedGames.Cards.IndexOf(this.gameObject)].GetComponent<GS_CARD>()._GAMEID;
        // Go to wait screen
        SceneManager.LoadScene(WaitScene);


    }
}
