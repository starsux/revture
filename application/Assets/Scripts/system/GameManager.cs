
using System;
using System.IO;
using UnityEngine;

public class GameManager
{
    public static RevtureGameData currentGame;

    public static void CreateNewGame()
    {
        // Generate game
        currentGame = RevtureGame.Generate();
    }

    public static void LoadGame(string id)
    {
        currentGame = RevtureGame.LoadFromFile(RevtureGame.STORAGEPATH + id);
    }
}

public class RevtureGame
{


    /// <summary>
    /// Path containing the stored games
    /// </summary>
    public static string STORAGEPATH
    {
        get
        {
            return Application.persistentDataPath.ToString() + "/data/";
        }
    }


    public RevtureGame()
    {
        // If Storage Path folder does not exist, create it
        if (!Directory.Exists(STORAGEPATH))
        {
            Directory.CreateDirectory(STORAGEPATH);
        }
    }

    /// <summary>
    /// Generate game from random seed and stores in device
    /// </summary>
    public static RevtureGameData Generate()
    {
        // Create new class
        RevtureGameData data = new RevtureGameData();

        //TODO: Set default values
        data.GAME_ID = Guid.NewGuid().ToString().Replace("-", "");

        // Convert Object to json
        string json = JsonUtility.ToJson(data);

        // Save in device
        string output_path = STORAGEPATH + data.GAME_ID;
        using (StreamWriter fs = new StreamWriter(output_path))
        {
            fs.Write(json);
        }

        return data;
    }

    /// <summary>
    /// Reads file of game
    /// </summary>
    /// <param name="game_id">Id of game to load</param>
    public static RevtureGameData LoadFromFile(string game_id)
    {
        string file_path = STORAGEPATH + game_id;
        RevtureGameData data = JsonUtility.FromJson<RevtureGameData>(file_path);
        return data;
    }
}
// Data for create serializable class
[Serializable]
public enum PlayableCharacters
{
    Stenpek,
    Arantia,
    Ren,
    Pikun
}


// Serializable class for store in json
[Serializable]
public class RevtureGameData
{
    // Unique id for every game stored
    public string GAME_ID;
    // Name setted by user
    public string GAME_NAME;
    // Gloabal seed for this game
    public string GAME_SEED;
    // Creation date
    public string CreationDate;

    // Last player position
    public Vector3 PlayerPosition;
    // Selected character by user in this game
    public PlayableCharacters PlayerCharacter;

}