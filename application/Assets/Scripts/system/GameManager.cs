
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager
{
    /// <summary>
    /// Define an instance for xurrent data of game loaded
    /// </summary>
    public static RevtureGameData currentGame;

    /// <summary>
    /// Set and generate new file with default data
    /// </summary>
    public static void CreateNewGame(string name)
    {
        // Generate game
        currentGame = RevtureGame.Generate(name);
    }

    /// <summary>
    /// Find and loads data of game in current instance
    /// </summary>
    /// <param name="id">Unique game file id</param>
    public static void LoadGame(string id)
    {
        currentGame = RevtureGame.LoadFromFile(RevtureGame.STORAGEPATH + id);
    }

    public static List<RevtureGameData> RetrieveAllStoredGames()
    {
        RevtureGame.CheckDirectory();

        // Get  all files in game storage path
        string[] files = Directory.GetFiles(RevtureGame.STORAGEPATH);

        // Define a revtureGame array for return loaded data
        List<RevtureGameData> loaded = new List<RevtureGameData>();

        // Load files in array
        foreach (string f in files)
        {
            RevtureGameData crt = RevtureGame.LoadFromFile(f);

            //Add to list
            loaded.Add(crt);
        }

        //return found data
        return loaded;
    }
}

public class RevtureGame
{
    public static bool CreationFinished = false;

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


    public static void CheckDirectory()
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
        CheckDirectory();
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

        // Creation finished
        CreationFinished = true;

        return data;
    }

    private static int GenerateSeed()
    {
        string seed = "";

        // Run the loop 10 times to create a longer, more random string
        for (int i = 0; i < 10; i++)
        {
            // Generate a random value between 0 and 1
            float randomValue = UnityEngine.Random.value;

            // Convert the value to a string and split it at the decimal point
            string[] splitValue = randomValue.ToString().Split(".");

            // Concatenate the first 5 digits after the decimal point to the seed string
            seed += splitValue[1].Substring(0, 5);
        }

        // Get first 7 digits
        seed = seed.Substring(0, 7);
        // Convert the seed string to an integer and return it
        return int.Parse(seed);
    }

    /// <summary>
    /// Reads file of game
    /// </summary>
    /// <param name="path">Id of game to load</param>
    public static RevtureGameData LoadFromFile(string path)
    {
        //Read content of file
        string json = File.ReadAllText(path);
        //Create an object from json
        RevtureGameData data = JsonUtility.FromJson<RevtureGameData>(json);
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


    /// <summary>
    ///  Save all data modified in specific game
    /// </summary>
    public static void SaveAll(RevtureGameData revdata)
    {
        // Convert Object to json
        string json = JsonUtility.ToJson(revdata);

        // Save in device
        string file_path = STORAGEPATH + revdata.GAME_ID;

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

    public override string ToString()
    {
        return $"ID: {this.GAME_ID} - NAME: {this.GAME_NAME}";
    }

    #region data
    public int SkillSlotsUnlocked = 0; // this includes power bar as 0
    public bool MapUnlocked = false;
    public bool InventoryUnlocked = false;
    // Unique id for every game stored
    public string GAME_ID;
    // Name selected by user
    public string GAME_NAME;
    // Gloabal seed for this game
    public int GAME_SEED;
    // Creation date
    public string CreationDate;
    // Time played
    public float GameSeconds;
    // Most played character
    public PlayableCharacters MSPlayerCharacter;
    // Persistent data for skills
    public List<PlayerSkills> _skilldata;

    //public RevtureGameData()
    //{
    //    foreach(var i in Enum.GetNames(typeof(PlayableCharacters)))
    //    {
    //        _skilldata.Add();
    //    }
    //}

    // Last player position
    public Vector3 PlayerPosition;
    // Last Selected character by user in this game
    public PlayableCharacters PlayerCharacter;

    /// <summary>
    /// Information about current part of game story
    /// </summary>
    public StoryControlSys StoryControl = new StoryControlSys();
    #endregion
}
