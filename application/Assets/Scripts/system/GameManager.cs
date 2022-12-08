
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
    public static RevtureGameData Generate(string game_name = "generic")
    {
        // Create new class
        RevtureGameData data = new RevtureGameData();

        //Set default values
        data.GAME_ID = Guid.NewGuid().ToString().Replace("-", "");
        data.GAME_NAME = game_name;
        data.GAME_SEED = GenerateSeed();
        data.CreationDate = DateTime.Now.Date.ToShortDateString();

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

    private static int GenerateSeed()
    {
        string seed = "";
        for(int i=0; i<1; i++)
        {
            seed += UnityEngine.Random.value.ToString().Split(".")[1];
        }

        return int.Parse(seed);
    }

    /// <summary>
    /// Reads file of game
    /// </summary>
    /// <param name="game_id">Id of game to load</param>
    public static RevtureGameData LoadFromFile(string game_id)
    {
        string file_path = STORAGEPATH + game_id;
        RevtureGameData data = JsonUtility.FromJson<RevtureGameData>(file_path);
        //Set data in current execution
        UnityEngine.Random.InitState(data.GAME_SEED);
        return data;
    }

    /// <summary>
    ///  Save all data modified in current game
    /// </summary>
    public static void SaveAll()
    {
        // Convert Object to json
        string json = JsonUtility.ToJson(GameManager.currentGame);

        // Save in device
        string file_path = STORAGEPATH + GameManager.currentGame.GAME_ID;

        File.WriteAllText(file_path, json);

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


/// <summary>
/// Class for read and write data of game
/// </summary>
[Serializable]
public class RevtureGameData
{

    #region data
    // Unique id for every game stored
    public string GAME_ID;
    // Name setted by user
    public string GAME_NAME;
    // Gloabal seed for this game
    public int GAME_SEED;
    // Creation date
    public string CreationDate;

    // Last player position
    public Vector3 PlayerPosition;
    // Selected character by user in this game
    public PlayableCharacters PlayerCharacter;
    #endregion
}