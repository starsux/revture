using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRuntime : MonoBehaviour
{
    public GameObject player;
    public string SpritesFolderPath;
    public Image PowerBar;

    // Start is called before the first frame update
    void Start()
    {
        // Set all playerparameters
        player.transform.position = GameManager.currentGame.PlayerPosition;
    }

    private void Update()
    {
        // Change color of bar
        PowerBar.color = player.GetComponent<PlayerManager>().CurrentColor;
        PowerBar.material.SetVector("glow_color", player.GetComponent<PlayerManager>().CurrentColor);
        PowerBar.material.color = player.GetComponent<PlayerManager>().CurrentColor;
    }

}
