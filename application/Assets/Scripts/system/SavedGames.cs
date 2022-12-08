using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ONLYDEBUG
        List< RevtureGameData> i =GameManager.RetrieveAllStoredGames();
        foreach(RevtureGameData c in i)
        {
            Debug.Log(c);
        }
    }

}
