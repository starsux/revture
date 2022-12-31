using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavedGames : MonoBehaviour
{
    public KeyCode ReturnKey;
    public ScrollRect sr;
    public RectTransform Canvas_Screen;
    public GameObject CardPrefab;
    public GameObject CardMessage;
    public RectTransform RTCardContainer;
    public Material gry;

    public static int CardSelected = 0;
    public static List<GameObject> Cards;
    private Vector3 DefaultCardSize;
    private float scrollOffset;
    public int ReturnSceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        // Fetch all game saved
        List<RevtureGameData> i = GameManager.RetrieveAllStoredGames();

        // Are there at least 1?
        if (i.Count > 0)
        {
            Cards = new List<GameObject>();
            // iterate in all fetched files
            foreach (RevtureGameData c in i)
            {
                // Spawn new card and get refrence
                GameObject tempcard = Instantiate(CardPrefab, RTCardContainer.gameObject.transform);
                // Set game name
                tempcard.GetComponent<GS_CARD>()._gamename = c.GAME_NAME;
                // Set game id
                tempcard.GetComponent<GS_CARD>()._GAMEID = c.GAME_ID;
                // Retrieve and set name of most played character
                tempcard.GetComponent<GS_CARD>()._mp_character = c.MSPlayerCharacter;
                // Get total seconds played of game
                System.TimeSpan tsp = System.TimeSpan.FromSeconds(c.GameSeconds);
                // Fromat and set time
                tempcard.GetComponent<GS_CARD>()._time_played = tsp.Minutes + ":" + tsp.Seconds;
                ApplyGrayScale(tempcard,true);
                Cards.Add(tempcard);
            }

            // Store current card size
            DefaultCardSize = Cards[0].GetComponent<RectTransform>().localScale;
            scrollOffset = Cards.Count * 1.2f;
            // update scroll limits
            RTCardContainer.sizeDelta = new Vector2(150 * scrollOffset, RTCardContainer.sizeDelta.y);

            // Move scroll to first card
            sr.normalizedPosition = new Vector2(0.5f,0);
        }
        else
        {
            // Spawn message card
            Instantiate(CardMessage, RTCardContainer.gameObject.transform);

        }


    }

    public void ApplyGrayScale(GameObject o, bool v)
    {
        var ToApplyGray = o.GetComponent<GS_CARD>().GrayGroup;
        foreach (var i in ToApplyGray)
        {
            if (v)
            {
                i.material = gry;
            }
            else
            {
                i.material = null;
            }

        }
    }

    private void FixedUpdate()
    {
        // Is user scrolling?
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            // Wait until scroll was zero
            sr.gameObject.GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            sr.gameObject.GetComponent<Image>().raycastTarget = true;
            // Calculate the new scroll position
            float scrollDelta = Input.GetAxisRaw("Mouse ScrollWheel");
            float newScrollPosition = sr.verticalNormalizedPosition - scrollDelta;

            // Clamp the new scroll position between 0 and 1
            newScrollPosition = Mathf.Clamp(newScrollPosition, 0, 1);

            // Set the scroll position
            sr.verticalNormalizedPosition = newScrollPosition;
        }
    }

    void Update()
    {

        if (Input.GetKeyUp(ReturnKey))
        {
            SceneManager.LoadScene(ReturnSceneIndex);
        }

    }



}
