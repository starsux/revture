using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuntime : MonoBehaviour
{
    public GameObject player;
    public string SpritesFolderPath;

    // Start is called before the first frame update
    void Start()
    {
        // Set all playerparameters
        player.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load(SpritesFolderPath + "/" + GameManager.currentGame.PlayerCharacter) as Sprite;

        //player.transform.position = GameManager.currentGame.PlayerPosition;
    }
}