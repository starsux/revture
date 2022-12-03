
using System;
using UnityEngine;

public class GameManager
{
    public static RevtureGame currentGame;

    public static RevtureGame CreateNewGame()
    {
        RevtureGame toReturn = new RevtureGame();
        toReturn.Generate();
        return toReturn;
    }

    public static RevtureGame LoadGame(string id)
    {
        RevtureGame toReturn = new RevtureGame();
        toReturn.LoadFromFile("saved/"+id+".dat");
        return toReturn;
    }
}

public class RevtureGame
{
    public string ID { get; set; }
    public Vector3 PlayerPosition { get; internal set; }

    public string PlayerCharacter { get; internal set; }

    //  Generate game from random
    internal void Generate()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reads and set parameters in this object from a file
    /// </summary>
    /// <param name="filepath">Path of file to load</param>
    internal void LoadFromFile(string filepath)
    {
        throw new NotImplementedException();
    }
}