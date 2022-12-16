using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGames : MonoBehaviour
{
    public GameObject CardPrefab;
    public GameObject CardMessage;
    public RectTransform RTCardContainer;
    public float IncreasingFactor = 1;

    private int CardSelected = 0;
    private List<GameObject> Cards;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch all game saved
        List<RevtureGameData> i = GameManager.RetrieveAllStoredGames();

        // Are there at least 1?
        if(i.Count > 0)
        {
            Cards = new List<GameObject>();
            // iterate in all fetched files
            foreach (RevtureGameData c in i)
            {
                // Spawn new card and get refrence
                GameObject tempcard = Instantiate(CardPrefab, RTCardContainer.gameObject.transform);
                // Set game name
                tempcard.GetComponent<GS_CARD>()._gamename = c.GAME_NAME;
                // Retrieve and set name of most played character
                tempcard.GetComponent<GS_CARD>()._mp_character = c.MSPlayerCharacter;
                // Get total seconds played of game
                System.TimeSpan tsp = System.TimeSpan.FromSeconds(c.GameSeconds);
                // Fromat and set time
                tempcard.GetComponent<GS_CARD>()._time_played = tsp.Minutes + ":" + tsp.Seconds;
                Cards.Add(tempcard);
            }
        }
        else
        {
            // Spawn message card
            GameObject tempcard = Instantiate(CardMessage, RTCardContainer.gameObject.transform);

        }


        // update scroll limits
        RTCardContainer.sizeDelta = new Vector2(150, RTCardContainer.sizeDelta.y);
    }

    void Update()
    {
        // Increase size of selected card
        Cards[CardSelected].GetComponent<RectTransform>().localScale = new Vector3(IncreasingFactor, IncreasingFactor, IncreasingFactor);

        //TODO: On mouse wheel, set card as selected

    }

}
