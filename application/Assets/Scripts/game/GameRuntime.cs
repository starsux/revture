using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuntime : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Set all  parameters
        player.transform.position = GameManager.currentGame.PlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
