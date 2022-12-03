using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TS_Actions : MonoBehaviour
{
    public Object WaitScene;
    public Object OptionsScene;
    public Object ContinueScene;
    public static string WStatus; // For wait screen while scene loading


    public void New_Game()
    {
        // New  game in mode normal (0)
        WStatus = "NEWGAME;0";
        SceneManager.LoadScene(WaitScene.name);
    }

    public void Continue_Game()
    {
        SceneManager.LoadScene(ContinueScene.name);
    }

    public void Game_Settings()
    {
        SceneManager.LoadScene(OptionsScene.name);
    }
}
