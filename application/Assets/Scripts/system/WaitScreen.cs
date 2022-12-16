using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitScreen : MonoBehaviour
{
    public string CinematicFileName;

    // Start is called before the first frame update
    void Start()
    {
        string[] args = TS_Actions.WStatus.Split(";");


        // Check Wait screen status
        if (args[0] == "NEWGAME")
        {
            //todo: Play cinematic  async while create new game

            GameManager.CreateNewGame();
            SceneManager.LoadScene("GAME");

        }else if (args[0] == "LOADGAME")
        {
            string game_id = args[1];
            GameManager.LoadGame(game_id);
        }
    }


}
